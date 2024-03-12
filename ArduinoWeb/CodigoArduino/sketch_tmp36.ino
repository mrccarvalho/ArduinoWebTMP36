#include <SPI.h>
#include <WiFiNINA.h>
#include <TMP36.h> //INCLUSÃO DE BIBLIOTECA

//INÍCIO - DEFINIÇÕES DE VARIÁVEIS E OBJETOS

///////colocar dados sensíveis em ficheiro separado/arduino_secrets.h
// Wi-Fi info - IDs e passwords
char ssid[] = "Galaxy-RC";          // Galaxy-RC / Senha : ttqo3746 network SSID (name)
char pass[] = "ttqo3746";   // network password : ttqo3746
int keyIndex = 0;                                // network key Index number
int status = WL_IDLE_STATUS;                     // Wifi radio's status

const int wifinum = 5;
int retries = 15;

bool connected = true;

//  site URL para onde serão enviados os dados
char* host = "192.168.61.243";
const int postPorta = 8087;

//  ID do dispositivo para inserir na base de dados
//  Se este dispositivo estiver na base de dados com o ID = 1, fazemos dispositivoId = "1"
String dispositivoId = "1";

// Variáveis globais que irão armazenar os valores dos sensores
float temp_c;

// Geralmente, devemos usar "unsigned long" para variáveis que armazenam tempo
unsigned long previousMillis = 0;        // irá armazenar a última vez que foi lido
const long interval = 2000;              // intervalo de leitura de cada sensor
const long postDuracao = 60000; //intervalo entre casa Post/Gravação na base de dados
unsigned long ultimoPost = 0;

TMP36 myTMP36(A0, 5.0);

//FIM - DEFINIÇÕES DE VARIÁVEIS E OBJETOS

/*
 * Método WIFICONNECT para efetuar a ligação a uma determinada rede
 * wi-fi através de ssid e password
 */
void wificonnect(char ssid[], char pass[]) {
  Serial.begin(9600); //INICIALIZA A SERIAL
    while (!Serial) {
    ; // espera pela porta série para conectar. 
  }
  delay(500);

  // verificar de existe o módulo wifi:
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("Communicação com o módulo wifi falhou!");
    // não continúa
    while (true);
  }
  // verificar se o firmware do módulo wireless está atualizado:
  String fv = WiFi.firmwareVersion();
  if (fv < WIFI_FIRMWARE_LATEST_VERSION) {
    Serial.println("Please upgrade the firmware");
  }

  // Tentativa de ligação à Rede Wifi:
  while (status != WL_CONNECTED) {
    Serial.print("Tentativa de ligação à Rede Wifi, SSID: ");
    Serial.println(ssid);
    status = WiFi.begin(ssid, pass);
    // espera 10 segundos para tentar ligar de novo:

    delay(10000);
  }

Serial.println(WiFi.localIP());
  // definimos a duração do último post e fazemos o post imediatamente
  // desde que o main loop inicía
  ultimoPost = postDuracao;
  Serial.println("Setup completo");
  Serial.println("");
  Serial.print("Está conectado à rede");
}

/*
 * obtém leituras/medições and armazena em variáveis globais
 */
void getMedicoes() {
  Serial.println(" - A obter Medições...");
  //VARIÁVEL QUE ARMAZENA A TEMPERATURA EM GRAUS CELSIUS
  //OBTIDA ATRAVÉS DA FUNÇÃO myTMP36.getTempC()
  temp_c = myTMP36.getTempC(); 
  // mostra as leituras no serial
  Serial.println(temp_c); 
}

//função que converte um endereço IP em string, para poder inserir na base de dados como string
 String IpAddressToString(const IPAddress& ipAddress)
{
    return String(ipAddress[0]) + String(".") +
           String(ipAddress[1]) + String(".") +
           String(ipAddress[2]) + String(".") +
           String(ipAddress[3]);
}
/* Método que realiza a tarefa de enviar as leituras 
 *para um serviço externo neste caso um servidor web  
 */
void post_data() {
  Serial.println("Post/Envio de Dados - Início ");
  //digitalWrite(READLED, HIGH);
  getMedicoes();
  Serial.print(" - A conectar-se a ");
  Serial.println(host);
  Serial.print(" - na porta ");
  Serial.println(postPorta);  
  
  // class WiFiClient para criar ligações TCP
  //É aqui  que faz a ligação com o ip do servidor na porta de destino
  WiFiClient client;
  if (!client.connect(host, postPorta)) {
    Serial.println(" - Ligação ao host falhou!");
    return;
  }

  //digitalWrite(ERRORLED, LOW);

  // Criar o URI para o request/pedido
  String url = String("/Home/PostDados") +
    String("?id=") + dispositivoId +
    String("&ip=") + IpAddressToString(WiFi.localIP()) +
    String("&temp_c=") + temp_c;
  Serial.println(" - A solicitar o URL: ");
  Serial.print("     ");
  Serial.println(url);
  
  // envia o request/pedido para o servidor
  client.print(String("GET ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" + 
               "Connection: close\r\n\r\n");
  delay(500);

  Serial.println(" - Resposta do Cliente: ");

  // Lê todas as linhas de resposta que vem do servidor web 
  // e escreve no serial monitor
  while(client.available()){
    String line = client.readStringUntil('\r');
    Serial.print(line);
  }
  
  Serial.println("");
  Serial.println(" - Closing connection");
  Serial.println("");
  
  //digitalWrite(READLED, LOW);
  Serial.println("Post/Envio Dados - FIM");
  Serial.println("");
}

void setup() {
   wificonnect(ssid, pass);
}

void loop() {
  if (true) {
    unsigned long diff = millis() - ultimoPost;
    if (diff > postDuracao) {
      post_data();
      ultimoPost = millis();
    }
  } else {
    //digitalWrite(ERRORLED, HIGH);
    Serial.println(" - Não conseguiu conectar-se ao host!");
    delay(1000);
    //digitalWrite(ERRORLED, LOW);
  }
}
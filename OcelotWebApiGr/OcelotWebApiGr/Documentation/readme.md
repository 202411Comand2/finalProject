Добовление нового Routes в файле Ocelot
```json
{
  "DownstreamPathTemplate": "/api/Shop/{everything}",
  "DownstreamScheme": "https",
  "DownstreamHostAndPorts": [
    {
      "Host": "localhost",
      "Port": 7171
    }
  ],
  "UpstreamPathTemplate": "/gateway/Shop/{everything}", 
  "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ], 
  "FileCacheOptions": {
    "TtlSeconds": 60
  }
}
```

* DownstreamPathTemplate - Путь в целевом микросервисе (например, /api/products)
* Протокол (http/https)
* Адреса сервисов (можно несколько для балансировки)
* UpstreamPathTemplate	Внешний URL, который видит клиент
* UpstreamHttpMethod	Разрешённые HTTP-методы (GET, POST и т.д.)

Используемые порта Ocelot:
* 7186 - продукты
* 7171 - магазин
* 7052 - избранные позиции

куда смотреть, чтобы понять на какой порт настроить docker:

```"Shop_Product_Service" -> "ClusterService" ->"ClusterAPI"->"Properties"->"secretsSettings.json"```

```json
{
  "SecretsSettings": {
    "ConnectionString": "Host=localhost;Port=5004;Database=ClusterService;Username=postgres;Password=;"
 }
```
* Host=localhost;
* Port=5004;

!порт будет измён

куда смотреть, чтобы понять на какой порт настроить ocelot:

```"Shop_Product_Service" -> "ClusterService" ->"ClusterAPI"->"Properties"->"launchSettings.json"```

```json
"https": {
   "commandName": "Project",
   "dotnetRunMessages": true,
   "launchBrowser": true,
   "launchUrl": "swagger",
   "applicationUrl": "https://localhost:7171;http://localhost:5248",
   "environmentVariables": {
     "ASPNETCORE_ENVIRONMENT": "Development"
   },
```
* https://localhost:7171; - смототрим сюда и прописываем её в ocelot
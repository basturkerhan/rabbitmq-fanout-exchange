### RabbitMQ ile Fanout Exchange yapısı kullanılarak oluşturulmuş Publisher-Consumer konsol uygulamasıdır.
### Dockerfile dosyaları içerisindeki ENV URI alanına RabbitMQ Cloud adresi yazılmalıdır.

### ./UdemyRabbitMQ.publisher
#### docker build -t no-exc-pub-img .
#### docker run --name no-exc-pub-con no-exc-pub-img

### ./UdemyRabbitMQ.subscriber
#### docker build -t no-exc-subs-img .
#### docker run --name no-exc-subs-con no-exc-subs-img

### RabbitMQ ile Fanout Exchange yapısı kullanılarak oluşturulmuş Publisher-Consumer konsol uygulamasıdır.
### Dockerfile dosyaları içerisindeki ENV URI alanına RabbitMQ Cloud adresi yazılmalıdır.

### ./UdemyRabbitMQ.publisher
#### docker build -t fanout-exc-pub-img .
#### docker run --name fanout-exc-pub-con fanout-exc-pub-img

### ./UdemyRabbitMQ.subscriber
#### docker build -t fanout-exc-subs-img .
#### docker run --name fanout-exc-subs-con fanout-exc-subs-img

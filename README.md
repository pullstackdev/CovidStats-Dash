## General info
RealTime Covid19 Chart web app with .net core 3.1 and signalR

## Table of contents
* core webapi projesi kuruldu
* codefirst ile  db işlemleri için (entity)class, context class eklendi, appsettings.json ve startup ayarları yapıldı
* paketler indirildi (3 tane) ve migration yapıldı, db oluştu
* covid hub oluşturuldu, chart bunun üzerinden beslenir
* startupa signalr düzenlemeleri eklendi
* db işlemleri/metodları için service klasörü ve içine class eklendi. Startupa bu servis scoped ile eklendi
* covid api controllerları oluşturuldu, endpointler burada olacak
* fake random data ve data ekleme oluşturuldu
* charta dataları doğru koymak için mssqld pivot table oluşturuldu
* pivottable dan dataları ado.net ile saf sorgu ile alan yapıyı kurduk
* client
* microsoft/signalr ekle ve htmle scripti taşı
* cors'u web urle göre ayarladık
* chart doldurduk htmlde ve invoke ile on yaptık

	
## Technologies
Project is created with:
* .NetCore 3.1 (C#)
* Html
* api
* services
* signalr
* javascrtip/html

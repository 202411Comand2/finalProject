# docker - compose бд

* [Images БД находится тут](https://github.com/IlyaGall/Docker-education/blob/main/FinalProject/costom_docker_bd/docker-compose.yml)
* Скачиваем его
* Открываем pgAdmin(от админа) переходим по пути где лежит файл docker-compose с помощью команды ```cd -path путь_до_файла_```
* После открытия собираем проект с помощью команды ```docker-compose up -d```


# структура проекта на данный момент

* APIGateway - Она служит посредником между пользователем и онлайн-сервисами, предоставляя единую точку входа и направляя запросы от клиентов туда, куда нужно.
* Client - С# примеры console клиента для быстрого тестирования сервисов
* IdentityService - не моё(что там знает Глеб)
* Cart_Order_Service - сервис для работы с закзами (корзина + заказы)
* Shop_Product_Service - сервисы для работы с магазином и его товаром без заказа
* Элементы решения - файлы readme
* API - отовизм из старой версии удалять рано
* BLL - отовизм из старой версии удалять рано
* DAL - отовизм из старой версии удалять рано
* docker -compose - файл сборки проекта
* Domain - отовизм из старой версии удалять рано
* Test - отовизм из старой версии удалять рано


# адреса сервисов

## занятиые порты 

* Сервис продуктов 7186
* Сервис магазина 7171
* Сервис избранных позиций 7052
* Сервис комментариев 7276
* Сервис кластер 7222
* Сервис кластер 5010
* Сервис корзины 7175
* Сервис заказы 7042

## ссылки на серсвисов

* [Сервис продуктов](https://localhost:7186/swagger/index.html)
* [Сервис магазина](https://localhost:7171/swagger/index.html)
* [Сервис избранных позиций](https://localhost:7052/swagger/index.html)
* [Сервис комментариев](https://localhost:7276/swagger/index.html)
* [Сервис кластер](https://localhost:7222/swagger/index.html)
* [Сервис корзины](https://localhost:7175/swagger/index.html)
* [Сервис заказов](https://localhost:7042/swagger/index.html)
* [Сервис gatway](http://localhost:5010/)


## ссылки для проверик ocelot

* http://localhost:5010/gateway/Product/1 - текст работы сервиса с продуктами Product
* http://localhost:5010/gateway/shop/1 - текст работы сервиса с shop* 


### Product API
#### get запросы

* http://localhost:5010/gateway/Product/GetAll - Получить всё продукты 
* http://localhost:5010/gateway/Product/GetShopProducts?ShopId=idShop" - Получить по id магазину (idshop заменить на текстовое поле)
* http://localhost:5010/gateway/Product/GetProductsByCluster?ClusterId=cluster - Получить по id кластеру (cluster заменить на текстовое поле)

#### post запросы (Добовление продукта)

```C#
 // модель 
 Models.Product.AddProductDto addProductDto = new();
 addProductDto.ShopId = 0;
 addProductDto.ClusterId = 0;
 addProductDto.NameProduct = "post ";
 addProductDto.Description = "description";
 addProductDto.Price = 0;
 addProductDto.Barcode = 0;
 addProductDto.ModelNumber = "modelNumber";
 //запрос
 http://localhost:5010/gateway/Product/add, content

```

#### Put запрос (Обновление продукта)

```C#
 
 // модель 
 Models.Product.UpdateProductDto UpdateProductDto = new();
 UpdateProductDto.ProductId = idResponseObject;
 UpdateProductDto.ClusterId = 0;
 UpdateProductDto.NameProduct = "put update";
 UpdateProductDto.Description = "description";
 UpdateProductDto.Price = 0;
 UpdateProductDto.Barcode = 0;
 UpdateProductDto.ModelNumber = "modelNumber";
 //запрос
 http://localhost:5010/gateway/Product/Update
           

```

#### delete (удаление продукта)

```C#
 // модель
 Models.Product.DeleteProductDto DeleteProductDto = new();
 DeleteProductDto.Id = idResponseObject;
 //запрос
 http://localhost:5010/gateway/Product/Delete
```


### Shop API

#### get 
!Отсутвует

#### POST(Создание\получить информацию о магазинах  list )

Создание магазина

```C#
  // модель
 Models.Shop.AddShopDto addShopDto = new();
 addShopDto.Name = "modelNumbersdadasdasdas";
 //запрос
 http://localhost:5010/gateway/Shop/add
```

```C#
 // модель
 Models.Shop.GetShopsInfoDto GetShopDto = new();
 GetShopDto.ShopIds = new List<int> { 1, 2, 3, 4, 5 };
 //запрос
 http://localhost:5010/gateway/Shop/GetInfo
```


#### put (Обновить название магазина,Восстановить магазин)

Обновить название магазина
```C#
// модель
 Models.Shop.UpdateShopDto UpdateShopDto = new();
 UpdateShopDto.Id = 1;
 UpdateShopDto.NewName = "Сам лучший магазин";
 //запрос
 http://localhost:5010/gateway/Shop/Update"
 ```
Восстановить магазин

```C#
// модель
Models.Shop.RestoreShopDto RestoreShopDto = new();
RestoreShopDto.Id = 1;
//запрос
http://localhost:5010/gateway/Shop/RestoreShop
```

 #endregion

#### delete (удаление магазина)

```C#
// модель
Models.Shop.DeleteShopDto deleteShopDto = new();
deleteShopDto.Id = 1;
//запрос
http://localhost:5010/gateway/Shop/Delete

```
### Favorite  API

####  get (Получить всё избранные позиции пользователя)

 * http://localhost:5010/gateway/Favorite/GetFavoriteUser?IdUser=intUserId -  intUserId - заменить на id пользователя

#### Post (добавление избранной позиции)
```C#
//модель
  Models.Favorite.AddFavoriteDto addFavoriteDto = new();
  addFavoriteDto.UserId = 12;
  addFavoriteDto.ProductId = 12;
  //запрос
  http://localhost:5010/gateway/Favorite/Add"
```
### Delete (избранной позиции)

```C#
  // модель
  Models.Favorite.DeleteFavoriteDto deleteFavoriteDto = new();
  deleteFavoriteDto.IdFavorite = 1;
 //запрос
  http://localhost:5010/gateway/Favorite/Delete"
```

### Comment API


* http://localhost:5010/gateway/Comment/GetCommentsProduct?IdProduct=IdShop; -  получить всё комментарии по магазину (IdShop - заменить на id магазина)

#### Post

дабавление комментария со стороны пользователей

```C#
   
     // добавление комментария
     Models.Comments.Comment.AddCommentDto addDto = new(1,"name",1,1,"commet text",12);
     http://localhost:5010/gateway/Comment/Add
```


// добавление комментария со стороны магазина
```C#
     Models.Comments.ReplyComment.AddReplyCommentDto addRepDto = new(2,"Сам такой");
     http://localhost:5010/gateway/CommentReply/Add
```


####  delete

delete удаление комметария со стороны пользователей

```C#
     Models.Comments.Comment.DeleteCommentDto deleteDto = new(1);
     http://localhost:5010/gateway/Comment/Delete
    
```

delete удаление комметария со стороны магазина

```C#
    
     Models.Comments.ReplyComment.DeleteCommentReplyDto deletdRepDto = new(1);
      http://localhost:5010/gateway/CommentReply/Delete
```

#### put
обновление комментария со стороны пользователей
```C#
     Models.Comments.Comment.UpdateCommentDto updateDto = new(1, "Новый комментарий",4);
     http://localhost:5010/gateway/Comment/Update
```

обновление комментария со стороны магазина
```C#
   Models.Comments.ReplyComment.UpdateCommentReplyDto updateRepDto = new(1, "Новый комментарий исправленый");
   http://localhost:5010/gateway/CommentReply/Update
```

###     Cluster API

#### get
         
* http://localhost:5010/gateway/Cluster/GetAllElements  - получить всё элементы кластера

* http://localhost:5010/gateway/Cluster/GetRootElements - получить всё корневые элементы кластера

Обновить название кластера
```C#
Models.Cluster.Clusters.GetClusterDto getClusterDto = new("Обновлённое название кластера");
http://localhost:5010/gateway/Cluster/GetChildrenElements?ClusterName={getClusterDto.ClusterName}
```

####  Post

добавление кластера

```C#
 Models.Cluster.Clusters.AddClusterDto addDto = new("Как-то имя кластера","");
 http://localhost:5010/gateway/gateway/Cluster/Add
```

#### put
обновление кластера
```C#
 Models.Cluster.Clusters.UpdateCluseterDto updateDto = new(1,"Обновлённое название кластера","");
 http://localhost:5010/gateway/Cluster/Update
```

#### delete 
удаление кластера
```C#           
 Models.Cluster.Clusters.DeleteClusterDto deleteDto = new(1);
 await DeleteAsync(deleteDto, "gateway/Cluster/Delete");
```

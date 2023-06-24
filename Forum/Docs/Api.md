# Authentication

## Register

```js
POST {{host}}/auth/register
```

## Register Request

```json
{
"firstName": "James",
"lastName": "McAvoy",
"username": "jmaccas",
"email": "jmac@gmail.com",
"password": "abigsecretpasswordguy123"
}
```

## Register Response

```json
{
"id": "d8dj3djd38-3d3d-k93d93kd939d-93d939dkd39kd93",
"firstName": "James",
"lastName": "McAvoy",
"username": "jmaccas",
"email": "jmac@gmail.com",
"token": "eefe...z9fkdk988djf"
}
```

## Login

```js
POST {{host}}/auth/login
```

## Login Request

```json
{
"email": "jmac@gmail.com",
"password": "abigsecretpasswordguy123"
}
```

## Login Response

```json
{
"id": "d8dj3djd38-3d3d-k93d93kd939d-93d939dkd39kd93",
"firstName": "James",
"lastName": "McAvoy",
"username": "jmaccas",
"email": "jmac@gmail.com",
"token": "eefe...z9fkdk988djf"
}
```


# Posting

## Creating a Post

```js
POST {{host}}/api/posts
```

## Create a Post Request

```json
{
"Title": "A title",
"Content": "This is the content of the post.",
"Tags": ["discussion", "etc."]
}
```

## Create a Post Response

```json
{
"Id": "kfk2fk2f239rmkl-13193mxzmdaskd-bmasfmaski31",
"Title": "A title",
"Content": "This is the content of the post.",
"Comments":[],
"Tags": ["discussion", "etc."],
"CreatedAt": "2023-06-10T12:34:56Z"
}
```

## Retrieving a Post

```js
GET {{host}}/api/posts/{id}
```

## Get a Post Request

```json
{
"Id": "kfk2fk2f239rmkl-13193mxzmdaskd-bmasfmaski31"
}
```

## Get a Post Response

```json
{
"Id": "kfk2fk2f239rmkl-13193mxzmdaskd-bmasfmaski31",
"Title": "A title",
"Content": "This is the content of the post.",
"Tags": ["discussion", "etc."],
"CreatedAt": "2023-06-10T12:34:56Z",
"author":{
    "id": "d8dj3djd38-3d3d-k93d93kd939d-93d939dkd39kd93",
    "firstName": "James",
    "lastName": "McAvoy",
    "userName": "jmaccas"
},
"Comments": ["hello", "great post"],
"Upvotes": 10,
"Downvotes": 3
}
```

## Sorting by Upvotes

```js
GET {{host}}/api/posts/top-likes?limit={Upvote.Count}&sort=desc
```

## Sorting by Comments

```js
GET {{host}}/api/posts/top-likes?limit={Comments.Count}&sort=desc
```

# Commenting

## Comment

```js
POST {{host}}/posts/{postId}/comments
```

## Comment Request

```json
"content": "This is a mystery comment."
```

## Comment Response
```js
201 Created
```
```json
"id": "0000000-000000-000-0000000-000000",
"content": "This is a mystery comment.",
"commenterId": "000000-000000-0000-00-0000000",
"postId": ""
```








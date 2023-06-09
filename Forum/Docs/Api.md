## Auth

### Register

```js
POST {{host}}/auth/register
```

### Register Request

```json
{
"firstName": "James",
"lastName": "McAvoy",
"username": "jmaccas",
"email": "jmac@gmail.com",
"password": "abigsecretpasswordguy123"
}
```

### Register Response

```json
{
"id": "d8dj3djd38-3d3d-k93d93kd939d-93d939dkd39kd93"
"firstName": "James",
"lastName": "McAvoy",
"username": "jmaccas",
"email": "jmac@gmail.com",
"token": "eefe...z9fkdk988djf"
}
```

### Login

```js
POST {{host}}/auth/login
```

### Login Request

```json
{
"email": "jmac@gmail.com",
"password": "abigsecretpasswordguy123"
}
```

## Login Response

```json
{
"id": "d8dj3djd38-3d3d-k93d93kd939d-93d939dkd39kd93"
"firstName": "James",
"lastName": "McAvoy",
"username": "jmaccas",
"email": "jmac@gmail.com",
"token": "eefe...z9fkdk988djf"
}
```
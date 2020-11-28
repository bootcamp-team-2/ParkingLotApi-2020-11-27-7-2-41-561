## ParkingLots

### POST /api/parkinglots

#### Request Body

```json
{
  "name": "",
  "capacity": 0,
  "location": ""
}
```

#### Response

- Status Codes: 201, 400
- HeaderLocation: GET /api/parkinglots/{id}

### GET /api/parkinglots

#### GET /api/parkinglots?name=&limit=&offset=

#### Response

- Status Codes: 200
- Body
  ```json
  [
    {
      "name": "",
      "capacity": 0,
      "location": ""
    }
  ]
  ```

### GET /api/parkinglots/{id}

#### Response

- Status Codes: 200, 404
- Body
  ```json
  {
    "name": "",
    "capacity": 0,
    "location": ""
  }
  ```

### DELETE /api/parkinglots/{id}

Response status codes: 204, 404

### PATCH /api/parkinglots/{id}

#### Request Body

```json
{
  "capacity": 1
}
```

#### Response

- Status codes: 204, 400, 404

---

## Orders

### POST /api/orders

#### Request Body

```json
{
  "parkinglotName": "",
  "plateNumber": ""
}
```

#### Response

- Status Codes: 201, 400
- HeaderLocation: GET /api/orders/{id}

### GET /api/orders/{id}

### Response

- Status Codes: 200, 404
- Body
  ```json
  {
    "orderNumber": "",
    "parkinglotName": "",
    "plateNumber": "",
    "creationTime": ""
  }
  ```

### PATCH /api/orders/{id}

#### Request Body

```json
{
    "status": "close"
}
```

#### Response

- status codes: 204, 400, 404
# rushcodingexercise
Coding exercise for job interview with Rush Enterprises

Created services using two different methods:

1- Basic .Net Core 6 WebApi, using traditional controllers on a docker container
2- Azure Functions

Both share common components, Data and Service Order. 
Where Data project/component contains the SQL Connector/Adapter using Dapper and the common data models as well as DTO objects.

- Created a SQL database in Azure
- Created Service Bus in Azure 

**WEB API**
  **Get Order**
    http://localhost:49156/Order?OrderNumber=ORD715770

  **Create Order**
    POST Method
    http://localhost:49156/Order
    Payload (application/json):
       {"customerId": 4}

  **Update Order**
    PATCH
    http://localhost:49153/Order
    Payload (application/json):
      {"orderNumber": "ORD715770","total": 100.25,"status": "Ordered"}
    
**AZURE FUNCTIONS**
  **Get Order**
  http://localhost:7071/api/GetOrderFunction?OrderNumber=ORD715770 

  **Create Order**
  POST Method
  http://localhost:7071/api/CreateOrder
  Payload (application/json):
     {"customerId": 4}

  **Update Order**
  PATCH Method
  http://localhost:7071/api/UpdateOrder
  Payload (application/json):
    {"orderNumber": "ORD715770","total": 100.25,"status": "Ordered"}
  
 In all methods, the services return the retrieved, created or updated record.
 [
    {
        "id": 9,
        "customerId": 4,
        "orderNumber": "ORD715770",
        "total": 100.25,
        "status": "Ordered",
        "createdDate": "0001-01-01T00:00:00+00:00",
        "updatedDate": "2021-12-02T16:28:11.18+00:00"
    }
]

**Error Handling**
I was able to add error handling to the Web Api Update Order method. 

**Logging**
Added basic logging, but this can also be isolated from the functions and controllers



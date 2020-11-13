# REST API

The API was made to answer specific questions and perform certain tasks. It has endpoints to meet those requirements as well as endpoints that were generated thanks to .NET EF scaffolds. Those other endpoints are still in the code even though they were not a part of the requirements as they might still come in handy later.

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/1ea9083c73ae0762bfd1)

## Required Endpoints

- Retrieving the current status of a specific Battery

  - GET /api/batteries/status/{id}

- Changing the status of a specific Battery
  - PUT /api/batteries/status/{id}
  - request body:

```javascript
{
  "Id": "{id}",
  "BatteryStatus": "NEW STATUS"
}
```

- Retrieving the current status of a specific Column

  - GET /api/columns/status/{id}

- Changing the status of a specific Column
  - PUT /api/columns/status/{id}
  - request body:

```javascript
{
  "Id": "{id}",
  "ColumnStatus": "NEW STATUS"
}
```

- Retrieving the current status of a specific Elevator

  - GET /api/elevators/status/{id}

- Changing the status of a specific Elevator
  - PUT /api/elevators/status/{id}
  - request body:

```javascript
{
  "Id": "{id}",
  "ElevatorStatus": "NEW STATUS"
}
```

- Retrieving a list of Elevators that are not in operation at the time of the request

  - GET /api/elevators/oos

- Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention

  - GET /api/buildings/interventions

- Retrieving a list of Leads created in the last 30 days who have not yet become customers.
  - GET /api/leads/outstanding

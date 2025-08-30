# OpenCrib.Backend 🏠🔗

**Backend API for the OpenCrib platform**

OpenCrib is a platform that lets anyone **host anything and find their people**—from house shows and art pop-ups to workshops and meetups. This repository contains the **C# .NET MVC REST API** backend that talks to **MongoDB**.

> ⚠️ **Status:** The previously deployed Azure API is **currently offline**. The endpoint URLs below reflect the intended routes but are not live at the moment.

---

## 🛠 Tech Stack

| Layer     | Technology                 |
| --------- | -------------------------- |
| Language  | C#                         |
| Framework | .NET 6+ (ASP.NET MVC)      |
| API Style | REST                       |
| Database  | MongoDB                    |
| Hosting   | Azure App Service (paused) |

---

## 🌐 Base URLs

* **Planned prod/dev (offline):**
  `https://opencribdevapi.azurewebsites.net/api/`

* **Local (default HTTPS):**
  `https://localhost:5001/api/`
  *(Check `Properties/launchSettings.json` for your actual ports.)*

---

## 📡 API Endpoints

### 👤 User Endpoints

| Method | Route                                  | Description                      |
| ------ | -------------------------------------- | -------------------------------- |
| GET    | `/User/GetAllUsers`                    | Get all registered users.        |
| GET    | `/User/FollowUser/{myUserId}/{userId}` | Follow another user.             |
| GET    | `/User/GetUser/{username}`             | Get user details by username.    |
| POST   | `/User/NewUser`                        | Register a new user.             |
| DELETE | `/User/Delete/{id}`                    | Delete a user by Mongo ObjectId. |

**Sample – Create User**

```
POST /api/User/NewUser
Content-Type: application/json

{
  "username": "wilsn",
  "displayName": "Wilson",
  "email": "wilson@example.com",
  "bio": "Host & dev",
  "photoUrl": "https://.../avatar.png"
}
```

---

### 🎉 Party/Event Endpoints

| Method | Route                                       | Description                                    |
| ------ | ------------------------------------------- | ---------------------------------------------- |
| POST   | `/Party/NewParty`                           | Create a new party/event.                      |
| POST   | `/Party/RSVP/{myUserId}/{partyId}`          | RSVP to an event.                              |
| POST   | `/Party/PostComment`                        | Post a comment on an event.                    |
| GET    | `/Party/GetPartiesNearby/{zipCode}/{range}` | List events near a ZIP within `range` (miles). |

**Sample – Create Party**

```
POST /api/Party/NewParty
Content-Type: application/json

{
  "title": "Backyard Jam",
  "hostUserId": "64f0c9a1e2...",
  "startTimeUtc": "2025-09-12T01:00:00Z",
  "endTimeUtc": "2025-09-12T04:00:00Z",
  "zipCode": "33199",
  "address": "123 Maple St, Miami, FL",
  "geo": { "lat": 25.756, "lng": -80.377 },
  "description": "Open mic + jam. Bring instruments.",
  "capacity": 40,
  "tags": ["music", "open-mic", "jam"]
}
```

**Sample – Post Comment**

```
POST /api/Party/PostComment
Content-Type: application/json

{
  "partyId": "64f0d01b6a...",
  "userId": "64f0c9a1e2...",
  "text": "Can I bring a keyboard?"
}
```

---

## 📝 Planned / Suggested Endpoints

* **Admission / Door Control** – Allow hosts to approve/deny attendees

  * `POST /Party/Admission/Approve`
  * `POST /Party/Admission/Deny`
* **Auth & Accounts** – Email/Password or OAuth (JWT)

  * `POST /Auth/Register`, `POST /Auth/Login`, `GET /Auth/Me`
* **Notifications** – RSVP confirmations, updates, reminders

  * `POST /Notifications/Send`
* **Media Uploads** – Party photos, flyers (SAS or pre-signed URLs)

---

## ⚙️ Configuration

### Environment Variables / `appsettings.json`

* Mongo connection (choose one pattern and keep it consistent):

  * `ConnectionStrings:Mongo` – full MongoDB URI
  * `MongoDb:ConnectionString` and `MongoDb:DatabaseName`
* ASP.NET:

  * `ASPNETCORE_ENVIRONMENT` = `Development` | `Production`

**Example `appsettings.Development.json`**

```
{
  "ConnectionStrings": {
    "Mongo": "mongodb://localhost:27017"
  },
  "MongoDb": {
    "DatabaseName": "opencrib_dev"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 🚀 Run Locally

1. **Clone**

   ```
   git clone https://github.com/yourusername/OpenCrib.Backend.git
   cd OpenCrib.Backend
   ```

2. **Restore & Build**

   ```
   dotnet restore
   dotnet build -c Development
   ```

3. **Run**

   ```
   dotnet run --launch-profile "https"
   ```

   The API should be available at `https://localhost:5001/api/`.

---

## 🧪 Quick Smoke Tests (cURL)

* Get all users

  ```
  curl -k https://localhost:5001/api/User/GetAllUsers
  ```

* Nearby events (ZIP + range miles)

  ```
  curl -k https://localhost:5001/api/Party/GetPartiesNearby/33199/15
  ```

---

## 🔒 Security (Roadmap)

* JWT-based authentication & role-based authorization (Host vs. Attendee)
* Input validation & schema constraints
* Rate limiting and request size limits
* CORS rules for web/iOS clients
* Indexes on common query fields (e.g., `username`, `zipCode`, `geo`)

---

## 🧩 Project Context

This backend powers **OpenCrib**, enabling:

* User management (create, delete, follow)
* Event creation & RSVP
* Location-based discovery
* Comments and engagement

---

## 📄 License

MIT License — free to use, modify, and learn from.

---

## 📣 Notes

* The Azure App Service deployment is **temporarily offline**; use **local** URLs for testing.
* When re-enabling hosting, update this README with the new base URL and any Swagger/Docs links.

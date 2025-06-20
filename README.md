# 🛒 E-Commerce Microservices System

A scalable and modular E-Commerce system built using **ASP.NET Core** and **Microservices Architecture**.  
Each service is designed with **Onion Architecture**, **Repository Pattern**, and full integration via **API Gateway**.

---

## 📌 Features

- 🧱 Microservices Architecture (4 independent APIs)
- 🧅 Onion Architecture in each service
- 💾 SQL Server with Entity Framework Core
- 🔐 JWT Authentication + Role Management
- 🔁 Resilient communication using **Polly**
- 📊 Centralized logging with **Serilog**
- 🚪 API Gateway using **Ocelot**
- 🧰 Manual Mapping for performance and flexibility
- 🧹 Global Exception Middleware + Request Routing Middleware

---

## 🧠 System Architecture

![Microservices Diagram](https://your-image-url.com/microservices-diagram.png)

> Each box represents an independent service.
> Requests flow from the client through the API Gateway, which routes traffic to each microservice using Ocelot.

---

## ⚙️ Tech Stack

| Layer | Technology |
|-------|------------|
| Backend | ASP.NET Core, EF Core, Onion Architecture |
| Auth | JWT + Role-based Authorization |
| Communication | API Gateway (Ocelot), Polly for retries |
| Logging | Serilog |
| Database | SQL Server |
| Mapping | Manual mapping (high-performance) |
| Middleware | Global Exception + Routing Middleware |

---

## 📁 Microservices

| Service        | Description                            |
|----------------|----------------------------------------|
| Auth Service   | Handles login, registration, JWT, roles |
| Product Service| Manages product catalog and inventory  |
| Order Service  | Handles orders, carts, and checkout    |
              

---

## 🖼️ Microservices Architecture Diagram

Below is a simplified architecture diagram of how the microservices interact with each other through the API Gateway:

![Microservices Architecture](https://your-image-url.com/microservices-diagram.png)

> Each service is independent, follows Onion Architecture, and communicates via the API Gateway.
> Services handle their own database and logic separately, allowing scalability and flexibility.

---






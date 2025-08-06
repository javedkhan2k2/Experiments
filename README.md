# 🔩 Clamping Dashboard – Beispielprojekt

Dies ist ein selbst entwickeltes Beispielprojekt zur Demonstration eines Dashboards zur Überwachung von Clamping-Daten. Es wurde im Rahmen meiner Bewerbung bei **HAINBUCH** erstellt, um meine Fähigkeiten in der Fullstack-Entwicklung mit **ASP.NET Core**, **Angular** und **SignalR** praxisnah darzustellen.

---

## 🎯 Ziel des Projekts

Ziel war es, eine moderne, wartbare und erweiterbare Dashboard-Anwendung zu bauen, die typische Komponenten eines industriellen Überwachungssystems abbildet:

- Geräteverwaltung
- Clamping-Daten-Analyse
- Live-Daten (per SignalR)
- Event-Log-Anzeige
- Warnungen & Systemstatus
- Navigation & Modularität

---

## 🛠️ Technologien

- **Backend**: ASP.NET Core 9, Entity Framework Core, Sqlite, Repository Pattern, AutoMapper
- **Frontend**: Angular 20, Bootstrap 5, Font Awesome
- **Live Feed**: SignalR (WebSocket-Kommunikation)
- **Sonstiges**: Bogus/Faker zum Erzeugen von Seed-Daten, Clean Architecture, DTOs

---

## 📸 Dashboard-Vorschau

### DashBoard

![Dashboard Screenshot](images/dashboard.png)

### Aktuelle Protokolle

![Protokolle](images/protokolle.png)

---

## 🔍 Features

- 🔢 **Gerätestatistiken**: Anzahl aktiver/inaktiver Geräte, Modelle usw.
- ⚙️ **Clamping-Zusammenfassung**: Letzte Clampings, Fehlversuche, Trends
- ⚠️ **Warnungen**: Visualisierung fehlgeschlagener Clampings (letzte 24h)
- 📋 **Event Logs**: Übersicht über Systemereignisse
- 📡 **Live Feed**: Simulierte Echtzeitdaten mit SignalR
- 🧭 **Navigation Cards**: Schnelle Navigation zu Unterseiten

---

## 🚀 Projekt starten

### Voraussetzungen

- .NET 9 SDK
- Node.js + Angular CLI
- Sqlite

### Schritte

```bash
# Backend
cd ClampingDevice
dotnet ef database update
dotnet run

# Frontend
cd Ngclient
npm install
ng serve

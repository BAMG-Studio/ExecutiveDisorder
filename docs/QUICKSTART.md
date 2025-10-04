# 🚀 Executive Disorder - Quick Start Guide

## 🎮 Play the .NET Console Game (with Saves!)

```bash
# Build
dotnet build

# Run
dotnet run --project ExecutiveDisorder.Game

# On startup, you'll see:
# 📁 Found saved games:
#   [1] autosave
#   [0] Start new game
# Choice: _

# Game auto-saves every 5 days to: saves/autosave.edsave
```

---

## 🐳 Start the Backend (Flask + PostgreSQL)

```bash
# One command to start everything:
docker-compose up -d

# Check it's running:
curl http://localhost:8000/health

# View logs:
docker-compose logs -f

# Stop:
docker-compose down
```

**Services:**
- Flask API: http://localhost:8000
- PostgreSQL: localhost:5432

---

## 🎯 Test the Backend API

```bash
# 1. Create a user
curl -X POST http://localhost:8000/api/users \
  -H "Content-Type: application/json" \
  -d '{"username":"player1","password":"test123"}'

# Response: {"id": 1, "username": "player1"}

# 2. Save a game
curl -X POST http://localhost:8000/api/saves \
  -H "Content-Type: application/json" \
  -d '{"user_id":1,"save_data":{"day":25,"score":1500}}'

# Response: {"id": 1}

# 3. Load saves
curl http://localhost:8000/api/saves/1

# Response: [{"id":1,"user_id":1,"save_data":{...},"created_at":"..."}]
```

---

## 🎨 Unity Integration

**Add CloudSaveManager to your scene:**

```csharp
// In your Unity script:
var cloudSave = GetComponent<CloudSaveManager>();

// Create user
StartCoroutine(cloudSave.CreateUser("player1", "pass123", (userId) => {
    Debug.Log($"User created: {userId}");
}));

// Save game
string gameData = JsonUtility.ToJson(yourGameState);
StartCoroutine(cloudSave.SaveGame(gameData, (saveId) => {
    Debug.Log($"Game saved: {saveId}");
}));

// Load saves
StartCoroutine(cloudSave.LoadSaves((saves) => {
    Debug.Log($"Saves: {saves}");
}));
```

---

## 📁 Project Structure

```
ExecutiveDisorderReplit/
├── ExecutiveDisorder.Game/     # .NET Console game
│   └── saves/                  # Save files here
├── app/                        # Flask backend
│   ├── app.py                 # API server
│   └── schema.sql             # Database schema
├── Assets/Scripts/Managers/    # Unity scripts
│   └── CloudSaveManager.cs    # Cloud save integration
└── docker-compose.yml          # Full stack deployment
```

---

## 🔧 Development Workflow

### .NET Console Development
```bash
# Make changes to code
# Build and test
dotnet build
dotnet run --project ExecutiveDisorder.Game

# Test saves
# Play for 5+ days → auto-save triggers
# Restart → load saved game
```

### Backend Development
```bash
# Edit app/app.py
# Restart container
docker-compose restart backend

# View logs
docker-compose logs -f backend

# Access database
docker-compose exec postgres psql -U postgres -d executive_disorder
```

### Unity Development
```bash
# 1. Start backend
docker-compose up -d

# 2. Open Unity project
unity-hub --projectPath "C:\Users\POK28\source\repos\ExecutiveDisorderReplit"

# 3. Add CloudSaveManager to scene
# 4. Test API calls in Play mode
```

---

## 🐛 Troubleshooting

### .NET Console Issues

**Problem:** Save files not found
```bash
# Check saves directory exists
ls saves/

# Manually create if needed
mkdir saves
```

**Problem:** Build errors
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Backend Issues

**Problem:** Docker not starting
```bash
# Check Docker is running
docker ps

# Rebuild containers
docker-compose down
docker-compose up --build
```

**Problem:** Database connection failed
```bash
# Check PostgreSQL is running
docker-compose ps

# View database logs
docker-compose logs postgres

# Restart database
docker-compose restart postgres
```

**Problem:** Port already in use
```bash
# Change ports in docker-compose.yml:
ports:
  - "8001:8000"  # Change 8000 to 8001
  - "5433:5432"  # Change 5432 to 5433
```

### Unity Issues

**Problem:** API calls failing
```bash
# 1. Check backend is running
curl http://localhost:8000/health

# 2. Check CORS is enabled (already done in app.py)

# 3. Check Unity console for errors
```

---

## 📊 What You Have Now

### ✅ .NET Console Game
- Full game with 101 cards, 8 characters, 10 endings
- Auto-save every 5 days
- Load game on startup
- JSON save format

### ✅ Flask Backend
- REST API for user accounts
- Cloud save storage
- PostgreSQL database
- Docker deployment

### ✅ Unity Integration
- CloudSaveManager script
- Ready to connect to backend
- User creation and authentication
- Cloud save/load

---

## 🎯 Next Steps

1. **Play the console game** - Test save/load
2. **Start the backend** - `docker-compose up -d`
3. **Test the API** - Use curl commands above
4. **Integrate Unity** - Add CloudSaveManager to scene
5. **Deploy** - Use docker-compose for production

---

## 📚 Documentation

- `README.md` - Main project overview
- `ENHANCEMENTS.md` - Detailed enhancement docs
- `WORKSPACE_DIGEST.md` - Complete workspace analysis
- `IMPLEMENTATION_STATUS.md` - .NET console status

---

## 🎉 You're Ready!

Everything is set up and ready to use:
- ✅ Save system working
- ✅ Backend API functional
- ✅ Unity integration ready
- ✅ Docker deployment configured

**Start playing and building!** 🚀

---

**Commands Cheat Sheet:**
```bash
# Play game
dotnet run --project ExecutiveDisorder.Game

# Start backend
docker-compose up -d

# Test API
curl http://localhost:8000/health

# Stop backend
docker-compose down
```

🎮 **Democracy: Optional. Chaos: Guaranteed. Cloud Saves: Enabled.** ☁️

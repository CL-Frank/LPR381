# Linear & Integer Programming Solver

A comprehensive cross-platform desktop application for solving linear and integer programming problems with advanced sensitivity analysis capabilities.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)
![F#](https://img.shields.io/badge/F%23-Core-378BBA)
![C#](https://img.shields.io/badge/C%23-UI-239120)
![Avalonia](https://img.shields.io/badge/Avalonia-11.3-8B44AC)

## 🎯 Overview

This application implements multiple optimization algorithms for solving linear and integer programming problems. Built with a hybrid F#/C# architecture, it combines functional programming for mathematical rigor with modern UI design for accessibility.

## 📸 Screenshots

### Problem Formulation
*Interactive constraint builder with algorithm selection and variable restrictions*
<img width="80%" alt="formulation" src="https://github.com/user-attachments/assets/b28820b0-be39-43bf-b3ff-4c69d8184aea" />

### Solution Results
*Comprehensive solution summary with optimal values*
<img width="1341" height="488" alt="summary" src="https://github.com/user-attachments/assets/19603e05-dece-4a71-89cc-2cf0722e50b8" />

### Algorithm Iterations
*Step-by-step tableau iterations showing the simplex algorithm progress*
<img width="1330" height="599" alt="iterations" src="https://github.com/user-attachments/assets/fccfa739-fe68-46f9-b60f-a0dd66335799" />

### Sensitivity Analysis
*Advanced sensitivity analysis with range calculations and shadow prices*
<img width="1329" height="516" alt="analysis" src="https://github.com/user-attachments/assets/29dff053-c6a6-473d-b3d0-f7062d30fc5a" />

## ✨ Features

### Optimization Algorithms
- **Primal Simplex Method** - Classical tableau-based approach
- **Revised Simplex Method** - Memory-efficient matrix operations  
- **Dual Simplex Method** - For dual-feasible problems
- **Branch & Bound** - Integer programming solver
- **Cutting Plane Method** - Integer programming with constraint generation
- **Knapsack Solver** - Specialized 0-1 knapsack optimization

### Problem Formulation
- Interactive constraint builder with dynamic management
- File-based problem loading (.txt format)
- Variable sign restrictions (positive, negative, unrestricted)
- Integer and binary variable constraints
- Automatic canonical form conversion

### Advanced Analysis
- **Sensitivity Analysis** - Range analysis for objective coefficients and RHS values
- **Shadow Prices** - Economic interpretation of constraints
- **Duality Analysis** - Primal-dual relationship verification
- **What-If Scenarios** - Add new activities and constraints
- **Parametric Analysis** - Impact assessment of parameter changes

### Visualization
- Step-by-step algorithm iterations with formatted tableaux
- Comprehensive solution summaries
- Real-time canonical form preview
- Export functionality for results

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Windows, macOS, or Linux

### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/LPR381.git
cd LPR381
```

2. Build the solution:
```bash
dotnet build
```

3. Run the application:
```bash
cd LPR381.UI
dotnet run
```

## 📖 Usage

### Manual Problem Input

1. Select an algorithm from the dropdown
2. Choose objective type (Max/Min)
3. Enter objective function (e.g., `3x1 + 2x2 + 5x3`)
4. Click "Update Variables" to set variable restrictions
5. Add constraints using the constraint builder
6. Click "Submit" to solve

### File-Based Input

1. Click "Load from File"
2. Select a .txt file with problem definition
3. Problem automatically populates the UI
4. Click "Submit" to solve

### File Format Example
```
max
3x1 + 2x2 + 5x3
2x1 + 3x2 + x3 <= 10
x1 + 2x2 + 3x3 <= 15
x1 + x2 + x3 <= 8
+
```

### Sensitivity Analysis

After solving:
1. Navigate to Results → Sensitivity Analysis
2. Select variable or constraint to analyze
3. Click "Display Range" to see allowable ranges
4. Enter new values to perform what-if analysis
5. View shadow prices for constraint interpretation

## 🏗️ Technical Architecture

### Project Structure
```
LPR381/
├── LPR381.Core/          # F# mathematical core
│   ├── Formulation.fs    # Problem formulation types
│   ├── PrimalSimplex.fs  # Primal simplex algorithm
│   ├── RevisedSimplex.fs # Revised simplex algorithm
│   ├── BranchAndBound.fs # Integer programming
│   ├── CuttingPlanes.fs  # Cutting plane method
│   ├── Knapsack.fs       # Knapsack solver
│   └── ResultAnalysis.fs # Sensitivity analysis
└── LPR381.UI/            # C# Avalonia UI
    ├── Core/             # Solver runners
    ├── Models/           # Data models
    ├── Solvers/          # Algorithm implementations
    └── MainWindow.axaml  # Main interface
```

### Technology Stack
- **Languages:** F# (algorithms), C# (UI)
- **Framework:** .NET 9.0
- **UI:** Avalonia 11.3 (cross-platform)
- **Math Library:** MathNet.Numerics 5.0
- **Architecture:** Clean separation with functional core

### Key Design Patterns
- Generic tree interface for algorithm exploration
- Solver registry for dynamic algorithm selection
- Immutable data structures with lazy evaluation
- Context-based sensitivity analysis caching
- F#/C# interop layer for seamless integration

## 🔧 Development

### Building from Source
```bash
dotnet restore
dotnet build --configuration Release
```

### Running Tests
```bash
# Test scripts are located in LPR381.Core/
dotnet fsi LPR381.Core/TestPrimalSimplex.fsx
dotnet fsi LPR381.Core/TestBranchAndBound.fsx
```

## 📊 Example Problems

### Linear Programming
```
Maximize: 3x1 + 2x2
Subject to:
  2x1 + x2 <= 100
  x1 + x2 <= 80
  x1 <= 40
  x1, x2 >= 0
```

### Integer Programming
```
Maximize: 5x1 + 4x2
Subject to:
  x1 + x2 <= 5
  10x1 + 6x2 <= 45
  x1, x2 >= 0 and integer
```

### Knapsack Problem
```
Maximize: 60x1 + 100x2 + 120x3
Subject to:
  10x1 + 20x2 + 30x3 <= 50
  x1, x2, x3 binary
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Authors

Developed as part of the LPR381 Operations Research course project.-
- Caydan Frank
- Brian Felgate


## 🙏 Acknowledgments

- MathNet.Numerics for mathematical operations
- Avalonia UI for cross-platform framework
- F# community for functional programming resources

## 📧 Contact

For questions or feedback, please open an issue on GitHub.

---

**Note:** This application is designed for educational and research purposes. For production optimization problems, consider specialized commercial solvers.

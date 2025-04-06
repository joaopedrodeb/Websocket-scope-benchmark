# WebSocketScope Benchmark

[![CI Status](https://github.com/joaopedrodeb/Websocket-scope-benchmark/actions/workflows/ci.yml/badge.svg)](https://github.com/joaopedrodeb/Websocket-scope-benchmark/actions)
[![Release](https://img.shields.io/github/v/release/joaopedrodeb/Websocket-scope-benchmark?label=release)](https://github.com/joaopedrodeb/Websocket-scope-benchmark/releases)
[![License](https://img.shields.io/github/license/joaopedrodeb/Websocket-scope-benchmark)](LICENSE)

Ferramenta de benchmark para testar conexões WebSocket em massa com monitoramento de uso de memória em tempo real.

# WebSocketScope Benchmark

Ferramenta de benchmark para testar conexões WebSocket em massa com monitoramento de uso de memória em tempo real.

### 🎯 Objetivos

- Simular milhares de conexões WebSocket
- Monitorar conexões ativas e uso de memória
- Exportar resultados para CSV
- Interface gráfica com Windows Forms
- Servidor embutido usando ASP.NET Core

### 🚀 Tecnologias
- .NET 8
- WinForms
- ASP.NET Core WebSocket
- xUnit (testes)
- C#

### 📦 Estrutura

### 📦 Estrutura do Projeto

```
WebSocketScope/
│
├── WebSocketScope/                # Projeto principal WinForms (interface gráfica)
│   ├── MainForm.cs                # Tela principal com controle de execução
│   ├── MainForm.Designer.cs      # Designer do formulário
│   └── Program.cs                # Inicialização do aplicativo
│
├── ServidorWebSocket/            # Servidor WebSocket embutido (ASP.NET Core)
│   └── Servidor.cs               # Módulo que escuta e responde conexões
│
├── WebSocketScope.Servicos/      # Lógica de negócio e utilitários
│   ├── BenchmarkService.cs       # Geração de logs de benchmark
│   └── CsvExporter.cs            # Escrita de arquivos CSV (log)
│
├── WebSocketScope.Tests/         # Testes automatizados com xUnit
│   ├── BenchmarkServiceTests.cs  # Testes de formatação de log
│   └── CsvExporterTests.cs       # Testes de exportação de CSV
│
├── logs/                         # Pasta criada dinamicamente com os benchmarks
│   └── log_YYYYMMDD_HHmmss/      # Subpastas com logs por execução
│       └── log.csv
│
├── .github/workflows/            # Workflows do GitHub Actions
│   ├── ci.yml                    # Pipeline de build e testes
│   └── release.yml               # Geração automática de release + merge
│
├── README.md
├── LICENSE
└── WebSocketScope.sln           # Solução do Visual Studio
```

### ✅ O que falta pra funcionar tudo?

| Item                   | Status                                                                 |
|------------------------|------------------------------------------------------------------------|
| CI rodando             | ✅ Já está configurado com `.github/workflows/ci.yml`                  |
| Release Tag (v1.0.0)   | ❌ **Ainda não criada** → crie uma tag `v1.0.0` na branch `release/1.x` |
| Licença visível        | ✅ `LICENSE` está presente                                              |
| Cobertura via Codecov  | ⚠️ Opcional – posso configurar se quiser                               |

---

### ✅ Próximos passos recomendados

1. **Suba uma tag** de release:

```bash
git checkout release/1.x
git tag v1.0.0
git push origin v1.0.0

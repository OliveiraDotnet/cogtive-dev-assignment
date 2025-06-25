#  Desafio Técnico - Cogtive

Este repositório contém a solução para o desafio técnico, abordando duas frentes principais:

- Frontend Web com **React + TypeScript**
- Aplicativo Mobile com **.NET MAUI**

## Tecnologias Utilizadas

### Frontend Web (React)
- Bootstrap (via classes utilitárias)
- Toast Notifications + Loading screen
- Filtros, ordenação e pesquisa dinâmica

### Mobile (.NET MAUI)
- MVVM com Fody
- SQLite + EF Core
- Injeção de Dependência
- Acesso à conectividade
- UI moderna e responsiva

## Funcionalidades Implementadas

### Frontend React
- Listagem de máquinas com busca e filtro por status
- Ordenação por nome, tipo e status
- Seleção de máquina para exibir dados de produção
- Feedback visual com loading e toast de erro

### App Mobile MAUI
- Tela inicial com status da conexão (Wi-Fi, 4G, offline)
- Seleção de máquina com detalhes
- Armazenamento local com EF Core + SQLite
- Sincronização offline com controle de pendências

### Obs: Aplicativo não está 100% funcional, o objetivo é mostrar a idéia proposta
---

## Testes e Simulações

- Simulação de falha de rede no app (MAUI)
- Teste unitario para a lista de máquinas (WEB)
- Toast de erro (WEB)
- Botão de sincronização offline simulado (com delay - MAUI) 

---

## Decisões Tomadas

- **Migração completa de xamarin para MAUI** pela robustez e familiaridade com a stack da empresa
- **.NET 8 + EF Core** pela robustez e familiaridade com a stack da empresa
- **SQLite** para persistência offline e simplicidade na prototipação
- **Fody.PropertyChanged** para MVVM mais limpo no MAUI

---

## Desafios Enfrentados

- Integração de EF Core no MAUI com sincronização offline
- Manter UX fluida mesmo com estados de carregamento ou falha

---


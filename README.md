# 💰 Wallet API - Gestión de Billeteras Digitales

**API REST** desarrollada en **.NET 8** con arquitectura limpia (Clean Architecture) y patron **CQRS** para la gestión de billeteras digitales y transacciones financieras.

## 🛠 Stack Tecnológico
- **Backend**: .NET 8
- **Arquitectura**: Clean Architecture
- **Patrones**: CQRS, SOLID (énfasis en SRP e Inyección de Dependencias)
- **Autenticación**: JWT Bearer
- **Base de Datos**: SQL Server
- **Validaciones**: FluentValidation
- **Middleware**: Custom middleware para manejo global de excepciones

## 🌟 Características Clave
- ✅ Autenticación segura con JWT
- ✅ Seed automático con usuario de prueba
- ✅ Validaciones robustas con FluentValidation
- ✅ Manejo centralizado de errores
- ✅ Separación clara de responsabilidades

## 🚀 Primeros Pasos

### Requisitos Previos
- .NET 8 SDK
- SQL Server 2019+
- (Opcional) Azure Data Studio / SQL Server Management Studio

## 📈 Mejoras Futuras
- Implementar Patrón Repository para mayor abstracción del acceso a datos
- Añadir descarga de historial de transacciones (PDF/CSV)
- Migrar a Identity Server para autenticación más robusta
- Implementar autorización por roles/permisos
- Adicionar borrado lógico para auditoría de datos

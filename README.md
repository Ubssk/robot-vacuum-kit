Robot Vacuum Kit (C#, Java, JS)
Сервисы: Vendor Connector на трех языках. Внутри: /health, /metrics, эндпоинт политики прошивок, unit и integration тесты, OpenAPI, docker-compose для локального запуска, Prometheus+Grafana.

Быстрый старт
- Требуется Docker/Docker Compose.
- Запуск всего стека:
  docker compose up -d --build
- Проверка:
  - C# http://localhost:5000/health/ready и /metrics
  - Java http://localhost:8081/actuator/health/readiness и /actuator/prometheus
  - JS http://localhost:8082/health/ready и /metrics
  - Prometheus http://localhost:9090
  - Grafana http://localhost:3000 (admin/admin)

Тесты
- Unit:
  - C#: dotnet test csharp
  - Java: mvn -q -f java/vendor-connector/pom.xml test
  - JS: (в каталоге js/vendor-connector) npm test
- Интеграционные (против запущенных сервисов):
  - cd tests/integration && npm i && npm test
- E2E (Postman/Newman):
  - newman run tests/e2e/postman/collection.json -e tests/e2e/postman/environment.json

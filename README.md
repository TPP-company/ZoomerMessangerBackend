# ZoomerMessangerBackend



# Apply migrations:
  ```
  add-migration init -Context AuthDbContext -OutputDir Persistence/Identity/Migrations
  update-database -Context AuthDbContext
  ```
  ```
  add-migration init -Context AppDbContext -OutputDir Persistence/App/Migrations
  update-database -Context AppDbContext
  ```
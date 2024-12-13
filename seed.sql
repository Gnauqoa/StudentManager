-- Seed data for 'users' table
INSERT INTO "public"."Users" as u ("Username", "Password", "FullName", "CreatedAt")
VALUES 
    ('admin', '123456', 'Admin', CURRENT_TIMESTAMP)

-- Seed data for 'students' table
INSERT INTO "public"."Students" ("Name", "Age", "Address", "Phone", "CreatedAt")
VALUES
    ('Alice Johnson', 20, '123 Main St, City, Country', '123-456-7890', CURRENT_TIMESTAMP),
    ('Bob Smith', 22, '456 Oak St, City, Country', '234-567-8901', CURRENT_TIMESTAMP),
    ('Charlie Brown', 21, '789 Pine St, City, Country', '345-678-9012', CURRENT_TIMESTAMP);

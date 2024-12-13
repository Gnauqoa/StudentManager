-- Seed data for 'users' table
INSERT INTO users (username, password, full_name, created_at)
VALUES 
    ('admin', 'password123', 'Admin User', CURRENT_TIMESTAMP)

-- Seed data for 'students' table
INSERT INTO students (name, age, address, phone, created_at)
VALUES
    ('Alice Johnson', 20, '123 Main St, City, Country', '123-456-7890', CURRENT_TIMESTAMP),
    ('Bob Smith', 22, '456 Oak St, City, Country', '234-567-8901', CURRENT_TIMESTAMP),
    ('Charlie Brown', 21, '789 Pine St, City, Country', '345-678-9012', CURRENT_TIMESTAMP);

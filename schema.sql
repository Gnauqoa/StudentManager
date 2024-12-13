CREATE TABLE users (
    id SERIAL PRIMARY KEY,  -- Tự động tăng ID cho người dùng
    username VARCHAR(100) NOT NULL UNIQUE,  -- Tên người dùng (unique)
    password VARCHAR(255) NOT NULL,  -- Mật khẩu
    full_name VARCHAR(255),  -- Họ tên người dùng
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Thời gian tạo
);

CREATE TABLE students (
    id SERIAL PRIMARY KEY,  -- Tự động tăng ID cho sinh viên
    name VARCHAR(255) NOT NULL,  -- Tên sinh viên
    age INT NOT NULL,  -- Tuổi sinh viên
    address TEXT,  -- Địa chỉ sinh viên
    phone VARCHAR(15),  -- Số điện thoại sinh viên
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Thời gian tạo sinh viên
);

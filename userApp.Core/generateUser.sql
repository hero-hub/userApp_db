TRUNCATE TABLE public."Users" RESTART IDENTITY;

-- Функция генерации
CREATE OR REPLACE FUNCTION generate_random_users(count INTEGER) RETURNS VOID AS $$
DECLARE
    i INTEGER;
    
    -- Имена
    first_names TEXT[] := ARRAY['John', 'Michael', 'Robert', 'David', 'James', 'Mary', 'Jennifer', 'Linda', 'Patricia', 'Elizabeth', 'William', 'Richard', 'Joseph', 'Thomas', 'Daniel', 'Emma', 'Olivia', 'Ava', 'Sophia', 'Isabella', 'Alexander', 'Benjamin', 'Mason', 'Ethan', 'Logan'];
    
    -- Фамилии
    last_names TEXT[] := ARRAY['Smith', 'Johnson', 'Williams', 'Brown', 'Jones', 'Miller', 'Davis', 'Garcia', 'Rodriguez', 'Wilson', 'Martinez', 'Anderson', 'Taylor', 'Thomas', 'Hernandez', 'Moore', 'Martin', 'Jackson', 'Thompson', 'White'];
    
    -- Домены для email
    domains TEXT[] := ARRAY['gmail.com', 'mail.ru', 'gmail.ru', 'mail.com'];
    
    -- Символы для генерации пароля
    symbols TEXT := 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%^&*()+=?{}|~`_/.-';
    
    -- Суффиксы для имен пользователей
    username_suffixes TEXT[] := ARRAY['123', '456', '789', '1', '2', '3', '2023', '2024', '99', '007', '42', 'qwerty', 'asdf', 'zxcv'];
BEGIN
    FOR i IN 1..count LOOP
        DECLARE
            first_name TEXT := first_names[1 + floor(random() * array_length(first_names, 1))];
            last_name TEXT := last_names[1 + floor(random() * array_length(last_names, 1))];
            suffix TEXT := username_suffixes[1 + floor(random() * array_length(username_suffixes, 1))];
            username TEXT; 
            email TEXT;
            password TEXT := '';  
            hashed_password TEXT := '';  
            j INTEGER;  
            domain TEXT := domains[1 + floor(random() * array_length(domains, 1))];  
            symbol_pos INTEGER;  
        BEGIN
            -- Формирование имени пользователя
            username := lower(first_name || '.' || last_name || suffix);
            IF length(username) > 16 THEN
                username := substring(username FROM 1 FOR 16);
            END IF;
            
            -- Формирование email
            email := lower(first_name || '.' || last_name || floor(random() * 1000)::TEXT || '@' || domain);
            
            -- Генерация пароля
            FOR j IN 1..(8 + floor(random() * 9)) LOOP
                symbol_pos := 1 + floor(random() * length(symbols))::INTEGER;
                password := password || substr(symbols, symbol_pos, 1);
            END LOOP;
            
            -- Хешинг пароля
            hashed_password := encode(digest(password, 'sha256'), 'base64');
            
            -- Запись в таблицу
            INSERT INTO public."Users" ("UserName", "Email", "Password")
            VALUES (username, email, hashed_password);
        END;
                
        IF i % 10000 = 0 THEN
            RAISE NOTICE 'Processed % records', i;
        END IF;
    END LOOP;
END;
$$ LANGUAGE plpgsql;

CREATE EXTENSION IF NOT EXISTS pgcrypto;

SELECT generate_random_users(500000);
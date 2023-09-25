create table members(
    id serial primary key,
    name text,
    age int
);

INSERT INTO members (name, age) VALUES ('Alice', 20);
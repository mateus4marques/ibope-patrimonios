create database Ibope
go

create database IbopeHist
go

use Ibope
create table marcas(
    id uniqueidentifier primary key, 
    nome varchar(100) unique, 
)

create table patrimonios(
    id uniqueidentifier primary key, 
    nome varchar(100) unique, 
    marca_id uniqueidentifier references marcas(id) not null, 
    descricao varchar(300), 
    numero_do_tombo varchar(100) not null
)
go

use IbopeHist

create table marca_events(
    id int identity(1,1) primary key, 
    evento varchar(32) not null, 
    data_evento datetimeoffset not null,
    marca_id uniqueidentifier not null, 
    nome varchar(100)    
)

create table patrimonio_events(
    id int identity(1,1) primary key, 
    evento varchar(32) not null, 
    data_evento datetimeoffset not null,
    patrimonio_id uniqueidentifier not null,
    nome varchar(100), 
    marca_id uniqueidentifier not null, 
    descricao varchar(300), 
    numero_do_tombo varchar(100) not null
)
go









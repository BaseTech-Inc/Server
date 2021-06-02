create database tupabd

use tupabd

create table usuario (
	userId int not null primary key identity,
	name VARCHAR(30) not null, 
	email VARCHAR(256) not null, 
	passwor VARCHAR(30) not null, 
	bankAccount VARCHAR(30) not null,
	idtipouser int foreign key (idtipouser) references tipouser (idtipouser) not null, 
)

create table tipouser (
	idtipouser int not null primary key identity, 
	nome varchar(30)
)

create table localizacao (
	idloca int not null primary key identity,
	latitude float (17) not null,
	longitude float (17)  not null
)

create table pais (
	idpais int not null primary key identity,
	idloca int foreign key (idloca) references localizacao (idloca) not null,
	nome varchar (30) not null
)

create table estado (
	idestado int not null primary key identity,
	idloca int foreign key (idloca) references localizacao (idloca) not null,
	idpais int foreign key (idpais) references pais (idpais) not null,
	nome varchar (30) not null
)

create table cidade (
	idcidade int not null primary key identity,
	idloca int foreign key (idloca) references localizacao (idloca) not null,
	idestado int foreign key (idestado) references estado (idestado) not null,
	nome varchar (30) not null
)

create table distrito (
	iddistrit int not null primary key identity,
	idloca int foreign key (idloca) references localizacao (idloca) not null,
	idcidade int foreign key (idcidade) references cidade (idcidade) not null,
	nome varchar (30) not null
)

create table histprevisao (
	idhistprev int not null primary key identity,
	iddistrit int foreign key (iddistrit) references distrito (iddistrit) not null,
	dia date not null,
	hora time not null,
	umidade float (10) not null,
	descr varchar(250) not null,
	tempmax float (7) not null,
	tempmin float (7) not null,
	sensterm float (7) not null
)

create table histuser (
	idhistuser int not null primary key identity,
	userId int foreign key (userId) references usuario (userId) not null,
	cordpartida int foreign key (cordpartida) references localizacao (idloca) not null,
	cordchegada int foreign key (cordchegada) references localizacao (idloca) not null,
	distperc float (25) not null,
	duracao time not null,
	rota varchar(300) not null
)

create table alerta (
	idalerta int not null primary key identity,
	idloca int foreign key (idloca) references localizacao (idloca) not null,
	iddistrit int foreign key (iddistrit) references distrito (iddistrit) not null,
	data date not null,
	hora time not null,
	descr varchar(100),
	transitividade bit not null,
	atividade bit not null
)

create table marcadores (
	idmarcadores int not null primary key identity,
	idloca int foreign key (idloca) references localizacao (idloca) not null,
	userId int foreign key (userId) references usuario (userId) not null,
	nome varchar(30) not null
)
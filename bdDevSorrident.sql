create database bdDevSorrident;
use bdDevSorrident;

-- drop database bdDevSorrident;

create table tbLogin(
codUsu int primary key auto_increment,
usuario varchar (50), 
senha varchar (8),
tipo char (1));

create table tbPaciente(
CodPac int primary key auto_increment,
NomePac varchar(100),
CpfPac varchar(14),
CepPac varchar(9),
EmailPac varchar(50),
TelefonePac varchar(24),
SexoPac varchar(2)
);

create table tbEspecialidade(
CodEsp int primary key auto_increment,
Especialidade varchar(50) unique
);

create table tbDentista(
CodDen int primary key auto_increment,
NomeDen varchar(50),
CodEsp int not null,
constraint foreign key (CodEsp) references tbEspecialidade (CodEsp)
);

create table tbAtendimento(
CodAten int primary key auto_increment,
DataAten varchar(10),
HoraDen varchar(5),
CodPac int not null,
CodDen int not null,
constraint foreign key (codPac) references tbPaciente (codPac),
constraint foreign key (codDen) references tbDentista (codDen)
);

insert into tbLogin values (default, 'user1', 'user123', 1);
insert into tbLogin values (default, 'adm2', 'admin123', 2);
insert into tbLogin values (default, 'admin', 'admin', 2);

desc tbLogin;

insert into tbEspecialidade values (default, 'Protético');
insert into tbEspecialidade values (default, 'Téc Protese Dentária');
insert into tbEspecialidade values (default, 'Ortodontia');
insert into tbEspecialidade values (default, 'Periodontia');
insert into tbespecialidade values (default, 'Cardiologia');

insert into tbDentista values (default, 'José', 1);
insert into tbDentista values (default, 'Maria', 2);
insert into tbDentista values (default, 'João', 3);
insert into tbDentista values (default, 'Ana', 4);

create view vw_ListaDentista as
select	t1.CodDen,
		t1.NomeDen,
        t2.Especialidade
from tbDentista t1
inner join tbEspecialidade t2 on t1.CodEsp = t2.CodEsp;

create view vw_ListaAtendimento as
select	t1.CodAten,
		t1.DataAten,
        t1.HoraDen,
        t2.NomePac,
        t3.NomeDen,
        t4.Especialidade
from tbAtendimento t1
inner join tbPaciente t2 on t1.CodPac = t2.CodPac
inner join tbDentista t3 on t1.CodDen = t3.CodDen
inner join tbEspecialidade t4 on t3.CodEsp = t4.CodEsp;

select * from tbLogin;
select * from tbPaciente;
select * from tbDentista;
select * from tbEspecialidade;
select * from tbAtendimento;

delete from tbAtendimento where codAten = 2;
delete from tbespecialidade where codEsp = 5;

select * from vw_ListaDentista;
select * from vw_ListaAtendimento;


select * from tbEspecialidade order by codEsp;

-- Fazer os Cadastros, Listar, Editar, Excluir de todas as tabelas
-- Faça o Inner Join das tabelas com chave-estrangeira
-- Permita que apenas os usuários do Tipo 2 façam cadastros, editem dados ou excluam.
-- Usuários do tipo 1 só podem consultar os dados, sem possibilidade de alteração.
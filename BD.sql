use Pymes
create table Usuario
(
IdUsuario int IDENTITY(1,1) PRIMARY KEY ,
IdUser nvarchar(128) not null,
NombresUsuario nvarchar(100) not null,
Contraseña nvarchar(100) not null,
activo bit not null,
FOREIGN KEY (IdUser) REFERENCES AspNetUsers (Id),
)
Create table Persona
(
IdPersona int IDENTITY(1,1) PRIMARY KEY ,
Nombre nvarchar(50) not null,
Apellido nvarchar(50) not null ,
Cedula nvarchar(50) not null,
Direccion nvarchar(50) not null,
activo bit not null
)
create table Proveedor
(
IdProveedor int IDENTITY(1,1) PRIMARY KEY ,
IdPersona int not null,
Nombres nvarchar(100) not null,
Correo nvarchar(100) not null,
Telefono int not null,
activo bit not null
FOREIGN KEY (IdPersona) REFERENCES Persona (IdPersona),
)
Create table Producto
(
IdProducto int IDENTITY(1,1) PRIMARY KEY ,
NombreProducto nvarchar(50) not null,
PrecioXUnidad money not null ,
Descripcion nvarchar(50) not null,
activo bit not null
)



Create table Venta
(
IdVenta int IDENTITY(1,1) PRIMARY KEY ,
IdPersona int not null,
IdUsuario int not null,
Fecha date not null,
Direccion nvarchar(50) not null,
CantidaPedita int not null,
Total int not null,
activo bit not null
FOREIGN KEY (IdPersona) REFERENCES Persona (IdPersona),
FOREIGN KEY (IdUsuario) REFERENCES Usuario (IdUsuario),
)
Create table DetalleVenta
(
IdDetalleVenta int IDENTITY(1,1) PRIMARY KEY ,
IdProducto int not null,
IdVenta int not null,
FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto),
FOREIGN KEY (IdVenta) REFERENCES Venta (IdVenta),

)

create table InventarioProducto
(
IdInventarioProcducto int IDENTITY(1,1) PRIMARY KEY ,
IdProducto int not null,
UnidadMedida nvarchar(50) not null,
ValorxUnidad money not null,
CantidaExistencia  int not null,
FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto),
)

Create table MateriaPrima
(
IdMatriaPrima int IDENTITY(1,1) PRIMARY KEY ,
IdProveedor int not null,
NombreMateriaPrima nvarchar(50) not null,
PrecioXUnidad money not null ,
Descripcion nvarchar(50) not null,
activo bit not null
FOREIGN KEY (IdProveedor) REFERENCES Proveedor (IdProveedor),
)

create table InventarioMateriaPrima
(
IdInventarioMateriaPrima int IDENTITY(1,1) PRIMARY KEY ,
IdMateriaPrima int not null,
UnidadMedida nvarchar(50) not null,
ValorxUnidad money not null,
CantidaExistencia  int not null,
FOREIGN KEY (IdMateriaPrima) REFERENCES MateriaPrima (IdMatriaPrima),
)

create table CuentasxPagarProveedor
(
Id int IDENTITY(1,1) PRIMARY KEY ,
IdProveedor int not null,
IdMateriaPrima int not null,
CantidadComprada int not null,
precio money not null,
Total money not null,
FOREIGN KEY (IdMateriaPrima) REFERENCES MateriaPrima (IdMatriaPrima),
FOREIGN KEY (IdProveedor) REFERENCES Proveedor(IdProveedor),
)

create table PagoManoObra
(
IdPagoManoObra int IDENTITY(1,1) PRIMARY KEY ,
IdPersona int not null,
HorasTrabajadas int not null,
PagoXHora money not null,
Total money not null,
FOREIGN KEY (IdPersona) REFERENCES Persona (IdPersona),
)
create table CostoProduccion
(
IdCosto int IDENTITY(1,1) PRIMARY KEY ,
IdProducto int not null,
IdMateriaPrima int not null,
Cantidad int not null,
PagoManoObra  money  not null,
ValorUnidad money not null,
PreciodeVenta money not null
FOREIGN KEY (IdMateriaPrima) REFERENCES MateriaPrima (IdMatriaPrima),
FOREIGN KEY (IdProducto) REFERENCES Producto (IdProducto),
)
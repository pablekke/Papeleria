use Comercio;

SELECT * FROM Usuarios;
SELECT * FROM Clientes;
SELECT * FROM Articulos order by Nombre;
SELECT * FROM Pedidos; 
SELECT * FROM PedidosComunes;
SELECT * FROM PedidosExpres;
SELECT * FROM LineasPedido;

--Precarga de datos

--Precarga de Usuarios
INSERT INTO Usuarios (EsAdmin, Nombre, Apellido, Email, Contrase�a, Contrase�aHasheada)
VALUES
(0, 'Diego', 'Gonz�lez', 'diego.gonzalez@gmail.com', '123', '$2a$10$flLk6IILS3/JAaGoRwVJquKHIWI3xlBG3MQx7Cj39U7MuGGSnQeki'),
(1, 'Luc�a', 'P�rez', 'lucia.perez@gmail.com', '123', '$2a$10$flLk6IILS3/JAaGoRwVJquKHIWI3xlBG3MQx7Cj39U7MuGGSnQeki'),
(0, 'Javier', 'Mart�nez', 'javier.martinez@gmail.com', '123', '$2a$10$flLk6IILS3/JAaGoRwVJquKHIWI3xlBG3MQx7Cj39U7MuGGSnQeki'),
(1, 'Sof�a', 'L�pez', 'sofia.lopez@gmail.com', '123', '$2a$10$flLk6IILS3/JAaGoRwVJquKHIWI3xlBG3MQx7Cj39U7MuGGSnQeki')

--Precarga de Clientes
INSERT INTO Clientes (Nombre, Apellido, Email, RazonSocial, RUT, Ciudad, Calle, NumeroPuerta, Distancia)
VALUES
('Diego', 'Fern�ndez', 'diego.fernandez@gmail.com', 'Fern�ndez S.A.', '212345678901', 'Montevideo', 'Av. Italia', 1234, 5.0),
('Valentina', 'Rodr�guez', 'valentina.rodriguez@gmail.com', 'Rodr�guez Ltda.', '212345678901', 'Salto', 'Calle Uruguay', 4321, 10.0),
('Mart�n', 'P�rez', 'martin.perez@gmail.com', 'Electr�nica P�rez', '212345678901', 'Paysand�', 'Blvr. Artigas', 1500, 15.0),
('Luc�a', 'Garc�a', 'lucia.garcia@gmail.com', 'Garc�a Importaciones', '212345678901', 'Maldonado', 'Paseo del Puente', 780, 25.0),
('Javier', 'L�pez', 'javier.lopez@gmail.com', 'L�pez y Asociados', '212345678901', 'Canelones', 'Av. de las Am�ricas', 555, 2.0),
('Sof�a', 'Gonz�lez', 'sofia.gonzalez@gmail.com', 'Gonz�lez Joyas', '212345678901', 'Tacuaremb�', 'Calle 18 de Julio', 678, 50.0),
('Juan', 'Ram�rez', 'juan.ramirez@gmail.com', 'Ram�rez Sports', '212345678908', 'Rivera', 'Calle Sarand�', 234, 35.0),
('Andrea', 'Morales', 'andrea.morales@gmail.com', 'Modas Morales', '212345678901', 'Durazno', 'Ruta 5', 111, 20.0),
('Santiago', 'Alvarez', 'santiago.alvarez@gmail.com', 'Alvarez Tech', '212345678902', 'Rocha', 'Camino del Vi�edo', 322, 42.0),
('Camila', 'Su�rez', 'camila.suarez@gmail.com', 'Su�rez Gourmet', '212345678901', 'Artigas', 'San Mart�n', 789, 8.0),
('Macarena', 'Su�rez', 'maca.suarez@gmail.com', 'Macarrones', '732345678401', 'Montevideo', 'Cerro', 2277, 150.0);

--Precarga de Articulos
INSERT INTO Articulos (Nombre, Descripcion, Codigo, Precio, Stock) VALUES
('Lapicera BIC', 'Lapicera de tinta azul duradera', '1234567890123', 20.50, 100),
('Cuaderno A4', 'Cuaderno de 200 hojas rayadas', '1234567890124', 120.00, 50),
('Marcador Sharpie', 'Marcador permanente negro', '1234567890125', 45.75, 80),
('Tijeras Maped', 'Tijeras de acero inoxidable de 21 cm', '1234567890126', 150.00, 30),
('Pegamento UHU', 'Pegamento instant�neo de 100 ml', '1234567890127', 99.99, 40),
('Caja de Clips', 'Caja con 100 clips met�licos', '1234567890128', 30.20, 150),
('Carpeta 3 Anillos', 'Carpeta dura para hojas tama�o carta', '1234567890129', 200.00, 25),
('Regla de Pl�stico', 'Regla transparente de 30 cm', '1234567890130', 35.00, 120),
('Pack de Post-its', 'Bloc de notas adhesivas, varios colores', '1234567890131', 85.50, 90),
('Sobre Manila', 'Sobre tama�o A4 de papel manila', '1234567890132', 12.00, 200),
('Grapadora Rexel', 'Grapadora robusta para 100 hojas', '1234567890133', 250.00, 20),
('Cinta Adhesiva', 'Rollo de cinta transparente de 50 m', '1234567890134', 45.50, 60),
('Fichas Bibliogr�ficas', 'Paquete de 100 fichas blancas', '1234567890135', 55.00, 110),
('Etiquetas Adhesivas', 'Paquete de 50 etiquetas para impresora', '1234567890136', 130.00, 40),
('Corrector L�quido', 'Botella de corrector de 20 ml', '1234567890137', 35.99, 80),
('L�pices de Colores', 'Caja de 24 l�pices de colores', '1234567890138', 175.00, 45),
('Calculadora Cient�fica', 'Calculadora con funciones avanzadas', '1234567890139', 300.00, 15),
('Comp�s de Precisi�n', 'Comp�s met�lico ajustable', '1234567890140', 120.00, 22),
('Bloc de Dibujo', 'Bloc A3 para dibujo t�cnico', '1234567890141', 200.00, 30),
('Portaminas 0.5mm', 'Portaminas de precisi�n para dibujo', '1234567890142', 60.00, 55);

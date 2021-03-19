delete from Estoque
delete from produto
delete from caixa
delete from venda

DBCC CHECKIDENT ('produto', RESEED, 0)
DBCC CHECKIDENT ('Estoque', RESEED, 0)
DBCC CHECKIDENT ('caixa', RESEED, 0)
DBCC CHECKIDENT ('venda', RESEED, 0)
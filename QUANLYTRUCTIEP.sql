alter session set "_ORACLE_SCRIPT"=true;
--QUAN LY TRUC TIEP
--Create view
CREATE OR REPLACE VIEW ATBMHTTT_VIEW_NHANVIEN AS
SELECT MANV,TENNV,PHAI,NGAYSINH,DIACHI,SODT,VAITRO,MANQL,PHG
FROM ATBMHTTT_TABLE_NHANVIEN;
/

CREATE OR REPLACE VIEW ATBMHTTT_VIEW_NHANVIEN_QUANLY AS
SELECT MANV,TENNV,PHAI,NGAYSINH,DIACHI,SODT,VAITRO,MANQL,PHG
WHERE MANQL = sys_context('userenv','session_user') OR MANV = sys_context('userenv','session_user');
FROM ATBMHTTT_TABLE_NHANVIEN;
/

CREATE OR REPLACE VIEW v_ShowAllPhongBan
AS
SELECT * FROM ATBMHTTT_TABLE_PHONGBAN;
/

CREATE OR REPLACE VIEW v_ShowAllDeAn
AS
select * from ATBMHTTT_TABLE_DEAN ORDER BY MADA ASC;
/

--create procedure
CREATE OR REPLACE PROCEDURE sp_GetDetalEmployee(EMP OUT SYS_REFCURSOR,Ma IN CHAR)
AS

BEGIN
    
    IF Ma = sys_context('userenv','session_user') then
        OPEN EMP FOR
        SELECT nv.*,pb.TENPB FROM ATBMHTTT_TABLE_NHANVIEN nv,ATBMHTTT_TABLE_PHONGBAN pb WHERE nv.PHG =pb.MAPB and  MANV =Ma;  
    ELSE    
        OPEN EMP FOR
        SELECT nv.*,pb.TENPB FROM ATBMHTTT_VIEW_NHANVIEN nv,ATBMHTTT_TABLE_PHONGBAN pb WHERE nv.PHG =pb.MAPB and  MANV = Ma;

    END IF;
END;

CREATE OR REPLACE PROCEDURE sp_GetPhanCong(EMP OUT SYS_REFCURSOR,Ma IN CHAR)
AS
BEGIN
    OPEN EMP FOR
    select pc.THOIGIAN, nv.TENNV, da.TENDA 
    from ATBMHTTT_TABLE_NHANVIEN nv,ATBMHTTT_TABLE_PHANCONG pc,ATBMHTTT_TABLE_DEAN  da 
    where pc.MANV = nv.MANV  and pc.MADA = da.MADA and nv.MANV = Ma;
END;

CREATE OR REPLACE PROCEDURE sp_UPDATEQUANLY(NS  NVARCHAR2, DC  CHAR,SDT  CHAR)
AS
BEGIN
    UPDATE ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN
    SET NGAYSINH=TO_DATE(NS,'DD/MM/YYYY'),
        SODT=SDT,
        DIACHI=DC
    WHERE MANV=sys_context('userenv','session_user');
END;
/

CREATE OR REPLACE PROCEDURE sp_GetRoom(Room OUT SYS_REFCURSOR,Ma IN CHAR)
AS
BEGIN
    OPEN Room FOR
    SELECT * FROM ATBMHTTT_TABLE_PHONGBAN WHERE MAPB = Ma;
END;
/



--------------------------------
--QUANLY_TRUCTIEP----------------------------------------------------------------------
CREATE ROLE ATBMHTTT_ROLE_QLTRUCTIEP;
CREATE USER NV007 IDENTIFIED BY 1;
GRANT CREATE SESSION TO NV007;
GRANT ATBMHTTT_ROLE_QLTRUCTIEP TO NV007;
--cẤP QUYỀN---------------------------------------------------------------------
--gant quyen select trenview,table
grant select on ATBMHTTT_VIEW_NHANVIEN to ATBMHTTT_ROLE_QLTRUCTIEP;
grant select on v_ShowAllPhongBan to ATBMHTTT_ROLE_QLTRUCTIEP;
grant select on v_ShowAllDeAn to ATBMHTTT_ROLE_QLTRUCTIEP;
--grant quyen update tren bang
GRANT UPDATE (NGAYSINH,SODT,DIACHI) on ATBM_20H3T_22.ATBMHTTT_TABLE_NHANVIEN to ATBMHTTT_ROLE_QLTRUCTIEP;
--grant quyen execute procedure
GRANT EXECUTE ON ATBM_20H3T_22.sp_GetDetalEmployee TO ATBMHTTT_ROLE_QLTRUCTIEP;
GRANT EXECUTE ON ATBM_20H3T_22.sp_GetPhanCong TO ATBMHTTT_ROLE_QLTRUCTIEP;
GRANT EXECUTE ON ATBM_20H3T_22.sp_UPDATEQUANLY TO ATBMHTTT_ROLE_QLTRUCTIEP;
GRANT EXECUTE ON ATBM_20H3T_22.sp_GetRoom TO ATBMHTTT_ROLE_QLTRUCTIEP;




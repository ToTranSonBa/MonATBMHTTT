CREATE OR REPLACE CONTEXT atbm_user_ctx USING set_atbm_user_ctx_pkg;

CREATE OR REPLACE PACKAGE set_atbm_user_ctx_pkg
IS
 PROCEDURE set_atbm_role_context; 
END set_atbm_user_ctx_pkg;

CREATE OR REPLACE PACKAGE BODY set_atbm_user_ctx_pkg
IS
    PROCEDURE set_atbm_role_context 
    IS 
         role_name nvarchar2(200);
         role_nv nvarchar2(50);
         PHG nvarchar2(50);
         MAQL nvarchar2(50);
    begin
        select vaitro, phg, maql into role_name, PHG, MAQL from ATBM_20H3T_22.atbmhttt_table_nhanvien WHERE MANV = SYS_CONTEXT('USERENV','SESSION_USER');
        IF (role_name = 'NHÂN VIÊN') THEN
            role_nv := 'ATBMHTTT_ROLE_NHANVIEN';
        ELSIF (role_name = 'QUẢN LÝ TRỰC TIẾP') THEN
            role_nv := 'ATBMHTTT_ROLE_QLTRUCTIEP';
        ELSIF (role_name = 'TRƯỞNG PHÒNG') THEN 
            role_nv := 'ATBMHTTT_ROLE_TRUONGPHONG';
        ELSIF (role_name = 'TÀI CHÍNH') THEN 
            role_nv := 'ATBMHTTT_ROLE_TAICHINH';
        ELSIF (role_name = 'NHÂN SỰ') THEN 
            role_nv := 'ATBMHTTT_ROLE_NHANSU';
        ELSIF (role_name = 'TRƯỞNG ĐỀ ÁN') THEN 
            role_nv := 'ATBMHTTT_ROLE_TRUONGDEAN';
        ELSIF (role_name = 'BAN GIÁM ĐỐC') THEN 
            role_nv := 'ATBMHTTT_ROLE_GIAMDOC';
        ELSE 
            role_nv := '';
        END IF;
        DBMS_SESSION.SET_CONTEXT('atbm_user_ctx', 'atbm_role', role_nv);
        DBMS_SESSION.SET_CONTEXT('atbm_user_ctx', 'atbm_phg', phg);
        DBMS_SESSION.SET_CONTEXT('atbm_user_ctx', 'atbm_maql', maql);
    end;
end;

CREATE OR REPLACE TRIGGER atbm_trigger_role_ctx
AFTER LOGON ON DATABASE
BEGIN
    set_atbm_user_ctx_pkg.set_atbm_role_context;
 EXCEPTION
    WHEN NO_DATA_FOUND THEN 
        dbms_output.put_line(SUBSTR(SQLERRM, 1 , 64));
end;

select sys_context('atbm_user_ctx', 'atbm_role') from dual;
select sys_context('atbm_user_ctx', 'atbm_phg') from dual;
select sys_context('atbm_user_ctx', 'atbm_maql') from dual;

declare
    role_name nvarchar2(2000);
begin
    for r in (select * from USER_ROLE_PRIVS) 
    LOOP
        role_name := CONCAT(CONCAT(role_name, '-'), r.GRANTED_ROLE);
    end loop;
    dbms_output.put_line(role_name);
end;

SET SERVEROUTPUT ON;

SELECT * FROM atbm_20h3t_22.atbmhttt_table_nhanvien;

SELECT * FROM DBA_ROLE_PRIVS WHERE GRANTEE = SYS_CONTEXT('USERENV', 'SESSION_USER');

alter session set "_ORACLE_SCRIPT"=true;

begin
DBMS_SESSION.SET_CONTEXT('hqtcsdl_user_ctx', 'hqtcsdl_user_phg', 'nv001');
end;

@echo on 
call docker-compose -f docker-compose-local.yml --compatibility up --build
PAUSE
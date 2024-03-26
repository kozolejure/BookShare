docker build -t book_api .
docker run -p 8088:8080 --name book_api book_api
docker tag book_api jurek01/book_api:latest  
docker push jurek01/book_api:latest 
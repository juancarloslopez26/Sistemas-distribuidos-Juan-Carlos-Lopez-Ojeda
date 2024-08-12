FROM golang:1.22 AS build
WORKDIR /app


COPY go.mod ./
COPY main.go ./

RUN go build -v -o holamundo main.go

FROM debian:latest

WORKDIR /app

COPY --from=build /app/holamundo .

EXPOSE 80

ENTRYPOINT ["./holamundo"]
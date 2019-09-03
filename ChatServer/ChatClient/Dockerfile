FROM node:10.16.3

WORKDIR /app

COPY . .

RUN npm install && \
    npm run build

COPY . /app

EXPOSE 3000

CMD [ "node", "server.js" ]
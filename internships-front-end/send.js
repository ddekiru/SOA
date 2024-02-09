//import { connect } from 'amqplib';
const { connect } = require('amqplib');

async function sendMessage() {
  const connection = await connect('amqp://localhost');
  const channel = await connection.createChannel();
  const queue = 'messages';
  const message = 'RabbitMQ is working with Node.js';
  
  await channel.assertQueue(queue, { durable: false });
  
  channel.sendToQueue(queue, Buffer.from(message));
}

sendMessage().catch(console.error);
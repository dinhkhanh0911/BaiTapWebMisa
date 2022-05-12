const mongoose = require('mongoose')
const Schema = mongoose.Schema;
const ObjectId = Schema.ObjectId;

const Table = new Schema({
  name:String,
  columns:Array,
});

module.exports = mongoose.model('Table', Table);
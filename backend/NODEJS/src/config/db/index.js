const mongoose = require('mongoose')

async function connect(){
    try{
        await mongoose.connect('mongodb://localhost:27017/MISA_DVKHANH')
        console.log("Ket nối database thành công")
    }
    catch(e){
        console.log("Ket nối database thất bại")
    }
}

module.exports = { connect }
const express = require('express')
const morgan = require('morgan')
const router = require('./routers')
const db = require('./config/db')
var cors = require('cors')
const app = express()
const port = 3003

//Http Logger
app.use(morgan('combined'))

//Ket noi database
db.connect()

app.use(cors())

app.use(express.json())

router(app)

app.listen(port,() => {
    
})
const tableRouter = require('./table.route.js')

function router(app){
   app.use('/table',tableRouter)
}

module.exports = router
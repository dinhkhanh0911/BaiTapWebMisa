const { response } = require('express')
const Table = require('../models/Table.js')

class TableController{

    get(req,res){
        console.log(req.body)
        if(!!req.params.slug){
            
            Table.findOne({ name : req.params.slug})
            .then(response => {
                res.status(200).json(response)
            })
            .catch(e => {
                res.status(400).json(e)
            })
        }
        else res.status(400).json("")
    }

    update(req,res){
        console.log(req.body)
        if(!!req.params.slug){
            
            Table.findOneAndUpdate({ _id : req.params.slug},req.body)
            .then(response => {
                res.status(200).json(response)
            })
            .catch(e => {
                res.status(400).json(e)
            })
        }
        else res.status(400).json("")
    }
}

module.exports = new TableController
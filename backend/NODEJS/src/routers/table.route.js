const express = require('express')
const router = express.Router()
const tableController = require('../app/controllers/TableController.js')


router.get('/:slug',tableController.get)
router.put('/:slug',tableController.update)

module.exports = router
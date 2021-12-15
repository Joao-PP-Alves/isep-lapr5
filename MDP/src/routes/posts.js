const express = require('express');
var mongoose = require('express');
var Post = require('../model/post.js');
var router = express.Router();

router.post('/', function(req, res) {
    var post = new Post();
    post.name = req.body.name;
    post.save(function(err) {
        if(err) res.send(err);
        res.json({ message: 'Post created!'});
    });
});
router.get('/', function(req,res){
    Post.find(function(err, posts){
        if(err) res.send(err);
        res.json(posts);
    });
});
module.exports = router;
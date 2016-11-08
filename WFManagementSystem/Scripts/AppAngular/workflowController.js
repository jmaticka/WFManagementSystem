(function () {
    "use strict";


    // Getting existing module
    angular.module("app-workflow")
        .controller("workflowController", workflowController);

    function workflowController() {

        var vm = this;

        vm.blocks = [];

        vm.newBlock = {};
        

        vm.initStart = function () {

            var blockType = angular.element('#blockType');
            //blockType.attr('disabled', true);
            var worker = angular.element('#worker');
            worker.attr('disabled', true);
        };

        vm.addBlock = function () {
            var blockToAdd = {
                name: vm.newBlock.name,
                description: vm.newBlock.description,
                blockType: vm.newBlock.blockType,
                nextBlocks: [{}]
            };
            if (vm.newBlock.worker)
                blockToAdd.worker = vm.newBlock.worker;
            vm.blocks.push(blockToAdd);

            if (angular.element('#blockType')[0].disabled)
                angular.element('#blockType').attr('disabled', false);
            if (angular.element('#worker')[0].disabled)
                var worker = angular.element('#worker').attr('disabled', false);

            vm.newBlock = {};



        };


    }



})();
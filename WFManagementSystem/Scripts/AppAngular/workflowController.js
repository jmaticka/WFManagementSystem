(function () {
    "use strict";

    // Getting existing module
    angular.module("app-workflow")
        .controller("workflowController", workflowController);

    function workflowController() {
        var vm = this;

        vm.blocks = [];
        vm.newBlock = {};
        vm.data = data;
        
        vm.initStart = function () {
            var blockType = angular.element('#blockType');
            blockType.attr('disabled', true);

            vm.newBlock.blockType = vm.data.blockTypes[0];//bind start block

            var worker = angular.element('#worker');
            worker.attr('disabled', true);
            //blockType.attr('disabled', true);
        };

        vm.addBlock = function () {
            var blockToAdd = {
                name: vm.newBlock.name,
                description: vm.newBlock.description,
                blockType: vm.newBlock.blockType,
                nextBlocks: [{}],
            };
            if (vm.newBlock.worker)
                blockToAdd.worker = vm.newBlock.worker;
            vm.blocks.push(blockToAdd);

            vm.data.blockTypes.splice(0, 1); // remove start block from array

            //enable user and block type selections
            angular.element('#blockType').attr('disabled', false);
            angular.element('#worker').attr('disabled', false);

            vm.newBlock = {};
        };
    }
})();
(function () {
    "use strict";


    // Getting existing module
    angular.module("app-workflow")
        .controller("workflowController", workflowController);


    function workflowController() {

        var vm = this;

        vm.blocks = [
        {
            name: "Start",
            description: "Start",
            worker: { id: "46e0693a-e8e2-494b-8fef-1046eb72baa8", name: "garant@test.com" },
            blockType: { id: "1", name: "start" },
            nextBlocks: [{}]

        }];

        vm.newBlock = {};

        vm.addBlock = function () {
            var blockToAdd = {
                name: vm.newBlock.name,
                description: vm.newBlock.description,
                worker: { id: vm.newBlock.worker.id, name: vm.newBlock.worker.name },
                blockType: { id: vm.newBlock.blockType.id, name: vm.newBlock.blockType.name },
                nextBlocks: [{}]
            };
            vm.blocks.push(blockToAdd);

        };

    }


})();
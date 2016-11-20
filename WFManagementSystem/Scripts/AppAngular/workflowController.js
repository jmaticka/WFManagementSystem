(function () {
    "use strict";

    // Getting existing module
    angular.module("app-workflow", [])
        .controller("workflowController", ['$scope','$http', workflowController])
        .directive("htmldiv", function ($compile, $parse) {
            return {
                restrict: 'E',
                link: function (scope, elm, attr) {
                    scope.$watch(attr.content, function () {
                        elm.html($parse(attr.content)(scope));
                        $compile(elm.contents())(scope);
                    }, true);


                }
            }
        });

    function workflowController($scope, $http) {
        var vm = this;

        


        vm.blocks = [];
        vm.normalBlocks = [];
        vm.paralellBlocks = [];
        vm.lastParallelType = "";

        vm.newWorkflow = {};
        vm.data = data;
        vm.parallels = [{
            name: "addBlockFormLeft",
            newBlock: {}
        }];
        vm.normalNum = 0;
        vm.parallelNum = 0;
        vm.isParallel = false;
        vm.changed = true;
        vm.htmlCode = '';

        $scope.$watchCollection('vm.blocks', function (blocks) {
            var isThere = blocks ? blocks.filter(function (e) { return e.blockType.name == 'konec'; }).length > 0 : false;
            $scope.newWorkflowForm.$setValidity('endBlock', isThere);
        }, true);


        vm.addParallel = function () {
            var parallel = {
                name: "addBlockFormRight",
                newBlock: {}
            };

            vm.parallels.push(parallel);

        };



        vm.initStart = function (parallel) {
            if (vm.blocks.length < 1) {
                var blockType = angular.element('#blockType');
                blockType.attr('disabled', true);

                parallel.newBlock.blockType = vm.data.blockTypes[0];//bind start block

                var worker = angular.element('#worker');
                worker.attr('disabled', true);
                //blockType.attr('disabled', true);
            }
        };


        vm.addBlock = function (parallel) {
            //var numberOfParallels = parallel.newBlock.blockType.name == "rozdělení" ? 2 : 1;
            //var blockClass = vm.parallels.length == 2 ? "col-lg-6" : "col-lg-12";

            var blockToAdd = BlockToAdd(parallel);

            if (parallel.newBlock.blockType.name == "sloučení") {
                vm.isParallel = false;
                vm.changed = true;
                var index = findIndex("sloučení", vm.data.blockTypes);
                vm.data.blockTypes.splice(index, 2, { id: index, name: "rozhodnutí" }, { id: index, name: "rozdělení" });
                
                if(vm.paralellBlocks[vm.parallelNum - 1][0][vm.paralellBlocks[vm.parallelNum - 1][0].length - 1]
                    .nextBlocks.length == 0){
                    vm.paralellBlocks[vm.parallelNum - 1][0][vm.paralellBlocks[vm.parallelNum - 1][0].length - 1]
                         .nextBlocks.push(blockToAdd);
                }
                if(vm.paralellBlocks[vm.parallelNum - 1][1][vm.paralellBlocks[vm.parallelNum - 1][1].length - 1]
                    .nextBlocks.length == 0) {
                    vm.paralellBlocks[vm.parallelNum - 1][1][vm.paralellBlocks[vm.parallelNum - 1][1].length - 1]
                         .nextBlocks.push(blockToAdd);
                }
            }

            if (!vm.isParallel) {
                if (vm.changed) {
                    vm.changed = false;
                    vm.htmlCode += addNormalDiv(vm.normalNum);
                    vm.normalBlocks.push([]);
                    vm.normalNum++;
                }
                vm.normalBlocks[vm.normalNum - 1].push(blockToAdd);
                if (vm.blocks[vm.blocks.length - 1] && vm.blocks[vm.blocks.length - 1].nextBlocks == 0) {
                    vm.blocks[vm.blocks.length - 1].nextBlocks.push(blockToAdd);
                }

            }

            if (vm.isParallel) {
                if (vm.changed) {
                    vm.changed = false;
                    vm.htmlCode += addParallelDivs(vm.parallelNum);
                    vm.paralellBlocks.push([]);
                    vm.paralellBlocks[vm.parallelNum].push([],[]);
                    vm.parallelNum++;
                }
                if(parallel.name == "addBlockFormLeft"){
                    blockToAdd.position = "left";
                    if (vm.paralellBlocks[vm.parallelNum - 1][0].length == 0) {
                        vm.blocks[vm.blocks.length - vm.paralellBlocks[vm.normalNum - 1][1].length-1]
                        .nextBlocks.push(blockToAdd);
                    }
                    else {
                        vm.paralellBlocks[vm.parallelNum - 1][0][vm.paralellBlocks[vm.parallelNum - 1][0].length - 1]
                         .nextBlocks.push(blockToAdd);
                    }
                    vm.paralellBlocks[vm.parallelNum - 1][0].push(blockToAdd);
                }

                if (parallel.name == "addBlockFormRight") {
                    blockToAdd.position = "right";
                    if (vm.paralellBlocks[vm.parallelNum - 1][1].length == 0) {
                        vm.blocks[vm.blocks.length - vm.paralellBlocks[vm.parallelNum - 1][0].length-1]
                        .nextBlocks.push(blockToAdd);
                    }
                    else {
                        vm.paralellBlocks[vm.parallelNum - 1][1][vm.paralellBlocks[vm.parallelNum - 1][1].length - 1]
                         .nextBlocks.push(blockToAdd);
                    }
                    vm.paralellBlocks[vm.parallelNum - 1][1].push(blockToAdd);
                }

            }

            if (parallel.newBlock.blockType.name == "rozdělení" || parallel.newBlock.blockType.name == "rozhodnutí") {
                vm.addParallel();
                vm.isParallel = true;
                vm.changed = true;
                if (parallel.newBlock.blockType.name == "rozdělení") {
                    vm.lastParallelType = "rozdělení";
                    
                }
                if (parallel.newBlock.blockType.name == "rozhodnutí") {
                    vm.lastParallelType = "rozhodnutí";
                    
                }
                var index = findIndex("rozdělení", vm.data.blockTypes);
                vm.data.blockTypes.splice(index, 1, { id: index, name: "sloučení" });
                index = findIndex("rozhodnutí", vm.data.blockTypes);
                vm.data.blockTypes.splice(index, 1);
            }


            if (vm.parallels.length > 1 && !vm.isParallel) {
                vm.parallels = [{
                    name: "addBlockFormLeft",
                    newBlock: {}
                }];
            }

            vm.blocks.push(blockToAdd);


            if (parallel.newBlock.blockType.name == "start") {
                vm.data.blockTypes.splice(0, 1);
                var index = findIndex("sloučení", vm.data.blockTypes);
                vm.data.blockTypes.splice(index, 1);
            }

            if (parallel.newBlock.blockType.name == "konec") angular.element('#control').addClass('hide');
                 

            //enable user and block type selections
            angular.element('#blockType').attr('disabled', false);
            angular.element('#worker').attr('disabled', false);

            parallel.newBlock = {};
        };

        vm.saveWorkflow = function () {
            vm.newWorkflow.blocks = vm.blocks;

            $http.post("/api/workflows/create", vm.newWorkflow)
                .then(function() {
                    //success
                        vm.newWorkflow = {};
                    },
                    function() {
                    //failure
                    });

        };
        vm.updateWorkflow = function() {
            $http.post("/api/workflows/update", vm.newWorkflow)
                .then(function() {
                        //success
                        vm.newWorkflow = {};
                    },
                    function() {
                        //failure
                    });
        };

        function addParallelDivs(val) {
            var res = '<div class="col-lg-6 paddingZero">'
                +'<div ng-repeat="block in vm.paralellBlocks[' + val + '][0]" class="form-group row block">'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Název bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.name}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Popis bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.description}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Výchozí uživatel</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.worker.name}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Typ bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.blockType.name}}</span>'
                        + '</div>'
                    + '</div>'
                + '</div>'
                + '</div>'
            + '<div class="col-lg-6 paddingZero">'
            + '<div ng-repeat="block in vm.paralellBlocks[' + val + '][1]" class="form-group row block">'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Název bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.name}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Popis bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.description}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Výchozí uživatel</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.worker.name}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Typ bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.blockType.name}}</span>'
                        + '</div>'
                    + '</div>'
                + '</div>'
            + '</div>';
            return res;
        };

        function addNormalDiv(val) {
            var res = '<div ng-repeat="block in vm.normalBlocks[' + val + ']" class="form-group row block clear">'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Název bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.name}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Popis bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.description}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Výchozí uživatel</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.worker.name}}</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="col-lg-6">'
                        + '<label class="control-label">Typ bloku</label>'
                        + '<div>'
                            + '<span class="form-control">{{block.blockType.name}}</span>'
                        + '</div>'
                    + '</div>'
                + '</div>';
            return res;
        };


        function findIndex(name, array) {

            for (var i = 0; i < array.length; i++) {
                if (array[i].name == name)
                    return i;
            }
            return null;

        };

        function BlockToAdd(parallel) {
            var blockToAdd = {
                name: parallel.newBlock.name,
                description: parallel.newBlock.description,
                blockType: parallel.newBlock.blockType,
                nextBlocks: []
            };

            if (parallel.newBlock.worker)
                blockToAdd.worker = parallel.newBlock.worker;

            return blockToAdd;
        };
    }
})();
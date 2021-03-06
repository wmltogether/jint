/// Copyright (c) 2012 Ecma International.  All rights reserved. 
/**
 * @path ch15/15.4/15.4.4/15.4.4.22/15.4.4.22-8-b-iii-1-26.js
 * @description Array.prototype.reduceRight - This object is the Arguments object which implements its own property get method (number of arguments equals number of parameters)
 */


function testcase() {

        var testResult = false;
        function callbackfn(prevVal, curVal, idx, obj) {
            if (idx === 1) {
                testResult = (prevVal === 2);
            }
        }

        var func = function (a, b, c) {
            Array.prototype.reduceRight.call(arguments, callbackfn);
        };

        func(0, 1, 2);
        return testResult;
    }
runTestCase(testcase);

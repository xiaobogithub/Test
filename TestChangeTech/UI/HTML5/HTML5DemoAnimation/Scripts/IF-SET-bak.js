//check IF expression
	var count = 0;
	function checkIf(str,XMLobj){
		var IFArr = [];
		IFArr[count]=[];
		//recursion to check all the "IF" expression
		if(str.match(/^IF[^IF\[ELSE]+\.\d+/)){	
			
			var temp = str.match(/^IF[^IF\[ELSE]+\.\d+/).toString();
			IFArr[count][0] = temp.split('{V:')[1].split('}')[0].trim();//variable
			IFArr[count][1] = temp.split('=')[1].split('GOTO')[0].trim();//value
			IFArr[count][2] = temp.split('GOTO')[1].split('.')[0].trim();//page sequence number
			IFArr[count][3] = temp.split('GOTO')[1].split('.')[1].trim(); //page number
			var ifEx = temp.split('}')[1].split('GOTO')[0].trim();
			
			for(var i=0; i<proV.length; i++){
				if(proV[i][0] == IFArr[count][0]){
					//fingure out the aim page
					ifEx = proV[i][2] + ifEx;
					if(eval(ifEx) == true){
						sequenceOrder = parseInt(IFArr[count][2]);
						pageOrder = parseInt(IFArr[count][3]);
						return false;//once sucessed, stop
					}
				}
			}
			str = str.split(temp)[1].trim();
			count+=1;
			checkIf(str,XMLobj);
		}else 
		// if all the "IF" condition failed, check "ELSE" Expression
		if(str.match(/^\[ELSE.*\]$/)){
			sequenceOrder = parseInt(str.split('GOTO')[1].split('.')[0].trim());
			pageOrder = parseInt(str.split('GOTO')[1].split('.')[1].trim());
		}
	}
	//check SET expression
	function checkSet(str,XMLobj){
		//recursion to eval all the SET experssions.
		if(str.match(/^SET[^SET]+/)){
			var temp =str.match(/^SET[^SET]+/).toString();
			
			//find out the variable
			var aimVar = temp.split('{V:')[1].split('}')[0].trim();
			
			//recursion(because it may have more than one variable) to transfer the firt expression of the first SET to a string can be evaled
			var setEx = temp.split('=')[1].trim();
			
			function checkSetEx(){
				if(setEx.match(/\{V[^\+\-\*\/]+\}/)){
					var tempVal = setEx.split('{V:')[1].split('}')[0];
					var val = $('input').filter(function(){return $(this).attr('id') == tempVal}).val();
					setEx=setEx.replace('{V:'+ tempVal + '}', val.toString());
					checkSetEx();
				}
			}
			checkSetEx();
			$(proV).each(function(i){
				if(proV[i][0] == aimVar){
					proV[i][2] = parseFloat(eval(setEx));
					alert('set ' +proV[i][0] + ' to ' + proV[i][2]);
				}
			});
			str = str.split(temp)[1];
			checkSet(str,XMLobj);
		}
	}
var ifFlag = false;//when it's true then check ELSE
		for(var i=0; i<arr.length; i++){
			//if it's 'IF' array
			if(arr[i][0].match(/^IF.+/)!=null){
				//check whether the if expression is true or false
				var temp = arr[i][0].split('IF')[1];
				
				//recorsion to instead all variables of the temp to values;
				function checkVar(){
					if(temp.match(/\{V:/)!=null){
						var tempVar = temp.split('{V:')[1].split('}')[0];
						
						for(var j=0; j<proV.length; j++){
							if(proV[j][0] == tempVar){
								var val = proV[j][2];
							}
						}
						temp=temp.replace('{V:'+ tempVar + '}', val.toString());
						checkVar();
					}
					
				}
				checkVar();
				
				//if true, excute the expression after 'IF'
				if(eval(temp) == true){
					for(var k=1; k<arr[i].length; k++){
						//if it's set
						if(arr[i][k].match(/^SET.+/) != null){
							var temp = arr[i][k].split('SET')[1].trim();
							function insteadVar(){
								if(temp.match(/\{V:/)!=null){
									var tempVar = temp.split('{V:')[1].split('}')[0];
									for(var l=0; l<proV.length; l++){
										if(proV[l][0] == tempVar){
											var val = proV[l][0];
										}
									}
									temp=temp.replace('{V:'+ tempVar + '}', val.toString());
									insteadVar();
								}
								
							}
							insteadVar();
							alert(temp);
						}
						
					}
				}
				
			}
			//check IF end
			
		}
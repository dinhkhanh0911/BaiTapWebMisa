
import {handeCheckTable,enableModal} from './style.js'

const employseesApi = 'http://amis.manhnv.net/api/v1/Employees'
const departmentApi = 'http://amis.manhnv.net/api/v1/Departments'

$(document).ready(function(){
    loadData(bindingDataEmployeesToTable)
    handleButtonTaskClick()
    handleAddClick()
    handleSubmitBtnClick()
})

function loadData(callback){
    fetch(employseesApi)
        .then(function(response){
            return response.json()
        })
        .then(function(employees){
            callback(employees)
        })
        .catch(function(error){
            console.log(error)
        })
    
}
function loadDepartmentData(callback){
    fetch(departmentApi)
        .then(function(response){
            return response.json()
        })
        .then(function(departments){
            callback(departments)
        })
        .catch(function(error){
            console.log(error)
        })
}
function loadEmployeeData(id,callback){
    fetch(`${employseesApi}/${id}`)
            .then(function(response){
                return response.json() 
            })
            .then(function(employee){
                callback(employee)
            })
            .catch(function(error){
                console.log(error)
            })
}
function handleAddClick(){
    $('#add-btn').click(function(){
        
        loadDepartmentData(function(departments){
            console.log($('#modalInfo [name=Department]'))
            var selectOptions = departments.map(function(department){
                console.log(department)
                return `
                    <option value="${department.DepartmentId}">${department.DepartmentName}</option>
                `
            })
            console.log($('#modalInfo [name=DepartmentId]'))
            $('#modalInfo [name=DepartmentId]').html(selectOptions.join(''))
            
            handleSelectBox()
            
        })
        enableModal("modalInfo")
    })
}
function handleSelectBox(){
    $('.select-selected').remove()
    $('.select-items').remove()
    var x, i, j, l, ll, selElmnt, a, b, c;
        x = document.getElementsByClassName("custom-select");
        l = x.length;
        for (i = 0; i < l; i++) {
          selElmnt = x[i].getElementsByTagName("select")[0];
          ll = selElmnt.length;
          /*for each element, create a new DIV that will act as the selected item:*/
          a = document.createElement("div");
          a.setAttribute("class", "select-selected");
          a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
          x[i].appendChild(a);
          /*for each element, create a new DIV that will contain the option list:*/
          b = document.createElement("DIV");
          b.setAttribute("class", "select-items select-hide");
          for (j = 1; j < ll; j++) {
            /*for each option in the original select element,
            create a new DIV that will act as an option item:*/
            c = document.createElement("DIV");
            c.innerHTML = selElmnt.options[j].innerHTML;
            c.addEventListener("click", function(e) {
                /*when an item is clicked, update the original select box,
                and the selected item:*/
                var y, i, k, s, h, sl, yl;
                s = this.parentNode.parentNode.getElementsByTagName("select")[0];
                sl = s.length;
                h = this.parentNode.previousSibling;
                for (i = 0; i < sl; i++) {
                  if (s.options[i].innerHTML == this.innerHTML) {
                    s.selectedIndex = i;
                    h.innerHTML = this.innerHTML;
                    y = this.parentNode.getElementsByClassName("same-as-selected");
                    yl = y.length;
                    for (k = 0; k < yl; k++) {
                      y[k].removeAttribute("class");
                    }
                    this.setAttribute("class", "same-as-selected");
                    break;
                  }
                }
                h.click();
            });
            b.appendChild(c);
          }
          x[i].appendChild(b);
          a.addEventListener("click", function(e) {
              /*when the select box is clicked, close any other select boxes,
              and open/close the current select box:*/
                e.stopPropagation();
              closeAllSelect(this);
              this.nextSibling.classList.toggle("select-hide");
              this.classList.toggle("select-arrow-active");
            });
        }
        function closeAllSelect(elmnt) {
            
          /*a function that will close all select boxes in the document,
          except the current select box:*/
          var x, y, i, xl, yl, arrNo = [];
          x = document.getElementsByClassName("select-items");
          y = document.getElementsByClassName("select-selected");
          xl = x.length;
          yl = y.length;
          for (i = 0; i < yl; i++) {
            if (elmnt == y[i]) {
                // console.log(elmnt)
              arrNo.push(i)
            } else {
              y[i].classList.remove("select-arrow-active");
            }
          }
        //   console.log(arrNo)
          for (i = 0; i < xl; i++) {
            // console.log(x[i])
            var abc = arrNo.indexOf(i)
            if (abc) {
                // console.log(abc)
              x[i].classList.add("select-hide");
            }
          }
        }
        /*if the user clicks anywhere outside the select box,
        then close all select boxes:*/
        document.addEventListener("click", closeAllSelect);
}
function bindingDataEmployeesToTable(employees){
    var thElements = document.querySelectorAll('#tThead th')
    var keyValues = [...thElements].map((item) => {
        var propname = $(item).attr('propname')
        
        return propname
    });
    var count = 1;
    var html = employees.map(employee =>{
        
        var tdElements = [...thElements].map((item) => {
            var key = $(item).attr('propname')
            var format = $(item).attr('type')

            if(key == undefined) return ''

            var value = employee[key]
            if(key == 'STT') {
                return `
                    <td value="${employee[format]}">${count}</td>
                `
            }
            if(value == null && value == undefined){
                return `
                    <td></td>
                `
            }
            if(format === 'date'){
                var date = new Date(employee[key])
                return `
                    <td>${date.getDate()}/${date.getMonth()+1}/${date.getFullYear()}</td>
                `
            }

            var tdElement = `
                
                <td>${employee[key]}</td>
            `
            
            
            return tdElement
        }).join('')
        count++
        return `
            <tr>
                <td>
                    <label class="container">
                        <input type="checkbox">
                        <span class="checkmark"></span>
                    </label>
                </td>
                ${tdElements}
            </tr>
        `
    })
    
    $('#tbody').append(html)
    handleEvenTable()
}
function handleEvenTable(){
    handeCheckTable()
    handleDbClickTable()
}

function handleDbClickTable(){
    $("#tbody tr").dblclick(function(e){
        e.preventDefault()
        const tdElement = $(this).find(`td:nth-child(${2})`)
        const id = $(tdElement).attr('value')
        console.log(id)
        loadEmployeeData(id,bindingDataEmployeesToForm)
        loadDepartmentData(function(departments){
            console.log($('#modalInfo [name=Department]'))
            var selectOptions = departments.map(function(department){
                return `
                    <option value="${department.DepartmentId}">${department.DepartmentName}</option>
                `
            })
            console.log($('#modalInfo [name=DepartmentId]'))
            $('#modalInfo [name=DepartmentId]').html(selectOptions.join(''))
            
            handleSelectBox()
            
        })
        
        
    })
}
function bindingDataEmployeesToForm(employee){
    enableModal("modalInfo")
    for(var attr in employee){
        var element = document.querySelector(`#modalInfo input[name = ${attr}]`)
        var value = employee[attr]
        if($(element).attr('type') === 'date'){
            var date = new Date(value)
            $(element).val(`${date.getFullYear()==1970?"":date.getFullYear()}-0${(date.getMonth()+1)%10}-${date.getDate()>=10?date.getDate():"0"+date.getDate()}`)
        }
        else $(element).val(employee[attr])
    }
}

function handleButtonTaskClick(){
    var taskElements = document.querySelector('.task')
    var taskButton = document.querySelectorAll('.task button')
    taskButton = [...taskButton]
    $(taskButton).click(function(){
        for(var taskItem of taskButton){
            if(this === taskItem) {
                $(taskItem).parent().addClass('task-active')
            }    
            else {
                
                $(taskItem).parent().removeClass('task-active')
            }
                
        }
    })
}
function handleSubmitBtnClick(){
    var el = document.querySelector('#addStaffBtn')
    $('#addStaffBtn').click(function(event){
        $('#addStaffForm').submit()
    })
   
}

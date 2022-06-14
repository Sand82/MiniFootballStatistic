function getValue(e, tournamentId, teamId) {
    e.preventDefault();

    let teamName = document.getElementById("Name-" + teamId).value;

    let model = { TournamentId: tournamentId, TeamId: teamId, TeamName: teamName };

    var token = $('#antiForgaryToken input[Name=__RequestVerificationToken]').val();

    console.log(token);

    $.ajax({
        type: "POST",
        url: "/api/TeamName",
        data: JSON.stringify(model),
        headers: {
            'X-ANTIF-TOKEN': token
        },
        success: function (data) {
            
        },
        contentType: 'application/json'
    });
}

function getResult(e, tournamentId, teamId, groupNumber, schemaLength) {
    e.preventDefault();

    let goals = document.getElementById("Velue-" + teamId).value;

    var token = $('#antiForgaryToken input[Name=__RequestVerificationToken]').val();

    console.log(token);

    let model = {
        TournamentId: tournamentId,
        TeamId: teamId,
        Goals: goals,
        GroupNumber: groupNumber,
        SchemaLength: schemaLength
    };    

    $.ajax({
        type: "POST",
        url: "/api/Result",
        data: JSON.stringify(model),
        headers: {
            'X-ANTIF-TOKEN': token
        },
        success: function (data) {
            
        },
        contentType: 'application/json'
    });
}

function seePlayerStats(e, tournamentId, teamId) {
    e.preventDefault();

    let id = e.target.id;    

    let aElement = document.getElementById(id);

    let model = { TournamentId: tournamentId, TeamId: teamId };

    var token = $('#antiForgaryToken input[Name=__RequestVerificationToken]').val();

    console.log(token);

    $.ajax({
        type: "POST",
        url: "/api/Table",
        data: JSON.stringify(model),
        headers: {
            'X-ANTIF-TOKEN': token
        },
        success: function (data) {

            var removeDiv = document.getElementById("mainDiv");

            if (removeDiv != null) {
                aElement.removeChild(removeDiv);
            }

            var mainDiv = document.createElement('div');
            mainDiv.setAttribute("id", "mainDiv")

            var table = document.createElement('table');
            var thead = document.createElement('thead');
            var tbody = document.createElement('tbody');
            var tr = document.createElement('tr');            

            var th2 = document.createElement('th');
            th2.textContent = 'Name';
            th2.style.padding = "6px";

            var th3 = document.createElement('th');
            th3.textContent = 'Goals';
            th3.style.padding = "6px";

            var th4 = document.createElement('th');
            th4.textContent = 'Assists';
            th4.style.padding = "6px";

            tr.appendChild(th2);
            tr.appendChild(th3);
            tr.appendChild(th4);

            thead.appendChild(tr);

            table.appendChild(thead);


            for (var i = 0; i < data.length; i++) {

                var tr = document.createElement('tr');

                var td2 = document.createElement('td');
                var td3 = document.createElement('td');
                var td4 = document.createElement('td');

                var text2 = document.createTextNode(data[i].name);
                var text3 = document.createTextNode(data[i].goals);
                var text4 = document.createTextNode(data[i].assists);

                td2.appendChild(text2);
                td3.appendChild(text3);
                td4.appendChild(text4);

                tr.appendChild(td2);
                tr.appendChild(td3);
                tr.appendChild(td4);

                tbody.appendChild(tr);
            }

            table.appendChild(tbody);

            mainDiv.appendChild(table);

            aElement.appendChild(mainDiv);
        },
        contentType: 'application/json'
    });
}

function hidePlayersStatisticMenu(e) {
    e.preventDefault();

    let id = e.target.id;

    let aElement = document.getElementById(id);

    var removeDiv = document.getElementById("mainDiv");

    if (removeDiv != null) {
        aElement.removeChild(removeDiv);
        aElement.textContent = "Players Info";
    }
}
function getValue(e, tournamentId, teamId) {
    e.preventDefault();

    let teamName = document.getElementById("Name-" + teamId).value;

    let model = { TournamentId: tournamentId, TeamId: teamId, TeamName: teamName };

    $.ajax({
        type: "POST",
        url: "/api/TeamName",
        data: JSON.stringify(model),
        success: function (data) {
            
        },
        contentType: 'application/json'
    });
}

function getResult(e, tournamentId, teamId, groupNumber, schemaLength) {
    e.preventDefault();

    let goals = document.getElementById("Velue-" + teamId).value;

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

    $.ajax({
        type: "POST",
        url: "/api/Table",
        data: JSON.stringify(model),
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

            var th3 = document.createElement('th');
            th3.textContent = 'Goa';

            var th4 = document.createElement('th');
            th4.textContent = 'Ass';

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
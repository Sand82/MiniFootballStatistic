function getValue(e, tournamentId, teamId) {
    e.preventDefault();

    let teamName = document.getElementById("Name-" + teamId).value;

    let model = { TournamentId: tournamentId, TeamId: teamId, TeamName: teamName };

    $.ajax({
        type: "POST",
        url: "/api/TeamName",
        data: JSON.stringify(model),
        success: function (data) {
            console.log("We are in the game!!!")
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

    console.log(schemaLength);

    $.ajax({
        type: "POST",
        url: "/api/Result",
        data: JSON.stringify(model),
        success: function (data) {
            console.log("We are in the game!!!")
        },
        contentType: 'application/json'
    });
}